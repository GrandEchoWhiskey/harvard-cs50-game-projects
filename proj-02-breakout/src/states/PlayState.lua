--[[
    GD50
    Breakout Remake

    -- PlayState Class --

    Author: Colton Ogden
    cogden@cs50.harvard.edu

    Represents the state of the game in which we are actively playing;
    player should control the paddle, with the ball actively bouncing between
    the bricks, walls, and the paddle. If the ball goes below the paddle, then
    the player should lose one point of health and be taken either to the Game
    Over screen if at 0 health or the Serve screen otherwise.
]]

PlayState = Class{__includes = BaseState}

--[[
    We initialize what's in our PlayState via a state table that we pass between
    states as we go from playing to serving.
]]
function PlayState:enter(params)
    self.paddle = params.paddle
    self.bricks = params.bricks
    self.health = params.health
    self.score = params.score
    self.highScores = params.highScores
    self.level = params.level
	
	self.hasKey = true
	for _, brick in pairs(self.bricks) do
		if brick.key then
			self.hasKey = false
			break
		end
	end
	
	self.balls = {
		params.ball
	}
	
	self.powers = {}

    self.recoverPoints = 5000
	self.hitsLastDrop = 0

    -- give ball random starting velocity
    self.balls[1].dx = math.random(-200, 200)
    self.balls[1].dy = math.random(-50, -60)
end

function PlayState:update(dt)
    if self.paused then
        if love.keyboard.wasPressed('space') then
            self.paused = false
            gSounds['pause']:play()
        else
            return
        end
    elseif love.keyboard.wasPressed('space') then
        self.paused = true
        gSounds['pause']:play()
        return
    end

    -- update positions based on velocity
    self.paddle:update(dt)
	
	for _, ball in pairs(self.balls) do
		ball:update(dt)
	end
	
	for _, power in pairs(self.powers) do
		power:update(dt)
	end
	
	for i=#self.powers, 1, -1 do
	
		if self.powers[i]:collides(self.paddle) then
		
			-- add balls power up
			if self.powers[i].skin == 9 then
			
				ball1 = self.powers[i]:addBall(self.paddle.x + (self.paddle.width / 2) - 2, self.paddle.y - 8)
				ball2 = self.powers[i]:addBall(self.paddle.x + (self.paddle.width / 2) - 6, self.paddle.y - 8)
				table.insert(self.balls, ball1)
				table.insert(self.balls, ball2)
				
			-- heal power up
			elseif self.powers[i].skin == 3 then
			
				self.health = self.powers[i]:heal(self.health)
			
			-- grow paddle power up
			elseif self.powers[i].skin == 5 then
			
				self.paddle.size, self.paddle.width = self.powers[i]:paddleUp(self.paddle.size, 4, self.paddle.width)			
			
			-- found key
			elseif self.powers[i].skin == 10 and not self.hasKey then
			
				for _, brick in pairs(self.bricks) do
				
					if brick.key then
						brick.found = true
					end
					
					self.hasKey = true
					
				end
			
			end
			
			table.remove(self.powers, i)
			
		end
		
	end

	for _, ball in pairs(self.balls) do
		if ball:collides(self.paddle) then
			-- raise ball above paddle in case it goes below it, then reverse dy
			ball.y = self.paddle.y - 8
			ball.dy = -ball.dy
			
			-- additional power up drop chance
			-- purpose: not to get stuck with the key brick
			self.hitsLastDrop = self.hitsLastDrop + 1

			--
			-- tweak angle of bounce based on where it hits the paddle
			--

			-- if we hit the paddle on its left side while moving left...
			if ball.x < self.paddle.x + (self.paddle.width / 2) and self.paddle.dx < 0 then
				ball.dx = -50 + -(8 * (self.paddle.x + self.paddle.width / 2 - ball.x))
			
			-- else if we hit the paddle on its right side while moving right...
			elseif ball.x > self.paddle.x + (self.paddle.width / 2) and self.paddle.dx > 0 then
				ball.dx = 50 + (8 * math.abs(self.paddle.x + self.paddle.width / 2 - ball.x))
			end

			gSounds['paddle-hit']:play()
		end
	end

    -- detect collision across all bricks with the ball
    for k, brick in pairs(self.bricks) do
		for _, ball in pairs(self.balls) do
			-- only check collision if we're in play
			if brick.inPlay and ball:collides(brick) then

				self.hitsLastDrop = self.hitsLastDrop + 1
				
				-- 10% base prob + 1% for each hit in brick or paddle
				if math.random(0, 100) <= self.hitsLastDrop + 10 then

					local hp = self.health >= 3
					local pd = self.paddle.size >= 4
					table.insert(self.powers, PowerUp(PowerUp:random(self.hasKey, hp, pd), brick.x, brick.y))
					self.hitsLastDrop = 0
					
				end

				-- add to score
				if not brick.key or brick.found then 
					self.score = self.score + (brick.tier * 200 + brick.color * 25)
				end

				-- trigger the brick's hit function, which removes it from play
				brick:hit()

				-- if we have enough points, recover a point of health
				if self.score > self.recoverPoints then
					-- can't go above 3 health
					self.health = math.min(3, self.health + 1)
					if self.paddle.size < 4 then
						self.paddle.size = self.paddle.size + 1
						self.paddle.width = self.paddle.width + 32
					end

					-- multiply recover points by 2
					self.recoverPoints = self.recoverPoints + math.min(100000, self.recoverPoints * 2)

					-- play recover sound effect
					gSounds['recover']:play()
				end

				-- go to our victory screen if there are no more bricks left
				if self:checkVictory() then
					gSounds['victory']:play()

					gStateMachine:change('victory', {
						level = self.level,
						paddle = self.paddle,
						health = self.health,
						score = self.score,
						highScores = self.highScores,
						ball = ball,
						recoverPoints = self.recoverPoints
					})
				end

				--
				-- collision code for bricks
				--
				-- we check to see if the opposite side of our velocity is outside of the brick;
				-- if it is, we trigger a collision on that side. else we're within the X + width of
				-- the brick and should check to see if the top or bottom edge is outside of the brick,
				-- colliding on the top or bottom accordingly 
				--

				-- left edge; only check if we're moving right, and offset the check by a couple of pixels
				-- so that flush corner hits register as Y flips, not X flips
				if ball.x + 2 < brick.x and ball.dx > 0 then
					
					-- flip x velocity and reset position outside of brick
					ball.dx = -ball.dx
					ball.x = brick.x - 8
				
				-- right edge; only check if we're moving left, , and offset the check by a couple of pixels
				-- so that flush corner hits register as Y flips, not X flips
				elseif ball.x + 6 > brick.x + brick.width and ball.dx < 0 then
					
					-- flip x velocity and reset position outside of brick
					ball.dx = -ball.dx
					ball.x = brick.x + 32
				
				-- top edge if no X collisions, always check
				elseif ball.y < brick.y then
					
					-- flip y velocity and reset position outside of brick
					ball.dy = -ball.dy
					ball.y = brick.y - 8
				
				-- bottom edge if no X collisions or top collision, last possibility
				else
					
					-- flip y velocity and reset position outside of brick
					ball.dy = -ball.dy
					ball.y = brick.y + 16
				end

				-- slightly scale the y velocity to speed up the game, capping at +- 150
				if math.abs(ball.dy) < 150 then
					ball.dy = ball.dy * 1.02
				end

				-- only allow colliding with one brick, for corners
				break
			end
		end
    end

    -- remove power ups
	for i=#self.powers, 1, -1 do
		if self.powers[i].y >= VIRTUAL_HEIGHT then
			table.remove(self.powers, i)
		end
	end
	
	-- if ball goes below bounds, revert to serve state and decrease health
	for i=#self.balls, 1, -1 do
		if self.balls[i].y >= VIRTUAL_HEIGHT then
			table.remove(self.balls, i)
		end
	end
	
	if  #self.balls == 0 then
	
		if self.paddle.size > 2 then

			self.paddle.size = self.paddle.size - 1
			self.paddle.width = self.paddle.width - 32

		end
		
		self.health = self.health - 1
		gSounds['hurt']:play()

		if self.health == 0 then
		
			gStateMachine:change('game-over', {
				score = self.score,
				highScores = self.highScores
			})
			
		else
		
			gStateMachine:change('serve', {
				paddle = self.paddle,
				bricks = self.bricks,
				health = self.health,
				score = self.score,
				highScores = self.highScores,
				level = self.level,
				recoverPoints = self.recoverPoints
			})
			
		end
		
	end

    -- for rendering particle systems
    for k, brick in pairs(self.bricks) do
        brick:update(dt)
    end

    if love.keyboard.wasPressed('escape') then
        love.event.quit()
    end
end

function PlayState:render()
    -- render bricks
    for k, brick in pairs(self.bricks) do
        brick:render()
    end

    -- render all particle systems
    for k, brick in pairs(self.bricks) do
        brick:renderParticles()
    end

    self.paddle:render()
	
	for _, ball in pairs(self.balls) do
		ball:render()
	end
	
	for _, power in pairs(self.powers) do
		power:render()
	end

    renderScore(self.score)
    renderHealth(self.health)

    -- pause text, if paused
    if self.paused then
        love.graphics.setFont(gFonts['large'])
        love.graphics.printf("PAUSED", 0, VIRTUAL_HEIGHT / 2 - 16, VIRTUAL_WIDTH, 'center')
    end
end

function PlayState:checkVictory()
    for k, brick in pairs(self.bricks) do
        if brick.inPlay then
            return false
        end 
    end

    return true
end