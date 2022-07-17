PowerUp = Class{}

function PowerUp:init(skin, x, y)
    self.width = 16
    self.height = 16

	self.x = x
	self.y = y
	
    self.dy = 25

    self.skin = skin
end

function PowerUp:random(key, hp, pd)

	local powerup = {9, 5, 3, 10}
	
	local max = 4
	
	if key then
		max = max - 1
		table.remove(powerup, 4)
	end
	
	if hp then
		max = max - 1
		table.remove(powerup, 3)
	end
	
	if pd then
		max = max - 1
		table.remove(powerup, 2)
	end
	
	local rand = math.random(1, max)
	
	return powerup[rand]
	
end

function PowerUp:addBall(x, y)

	ball = Ball(math.random(7))
		
	ball.x = x
	ball.y = y
	ball.dy = math.random(50, 200)
	
	return ball
	
end

function PowerUp:heal(health)
	
	gSounds['recover']:play()
	return math.min(3, health + 1)
	
end

function PowerUp:paddleUp(size, max_size, width)

	if size < max_size then
	
		size = size + 1
		width = width + 32
	
	end
	
	return size, width

end

function PowerUp:collides(target)

    if self.x > target.x + target.width or target.x > self.x + self.width then
        return false
    end

    if self.y > target.y + target.height or target.y > self.y + self.height then
        return false
    end 

    return true
end


function PowerUp:update(dt)
    self.y = self.y + self.dy * dt
end

function PowerUp:render()
    love.graphics.draw(gTextures['main'], gFrames['powers'][self.skin],
        self.x, self.y)
end