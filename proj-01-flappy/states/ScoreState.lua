--[[
    ScoreState Class
    Author: Colton Ogden
    cogden@cs50.harvard.edu

    A simple state used to display the player's score before they
    transition back into the play state. Transitioned to from the
    PlayState when they collide with a Pipe.
]]

ScoreState = Class{__includes = BaseState}

--[[
    When we enter the score state, we expect to receive the score
    from the play state so we know what to render to the State.
]]

-- List must be sorted by score to work properly
trophy = {
	['bronze'] = {
		['score'] = 5,
		['img'] = 'trophy/bronze.png'
	},
	['silver'] = {
		['score'] = 10,
		['img'] = 'trophy/silver.png'
	},
	['gold'] = {
		['score'] = 20,
		['img'] = 'trophy/gold.png'
	},
	['diamond'] = {
		['score'] = 40,
		['img'] = 'trophy/diamond.png'
	}
}

function ScoreState:enter(params)
    self.score = params.score
	self.trophy = nil
	
	for key, value in pairs(trophy) do
		if self.score >= value['score'] then
			self.trophy = value['img']
		end
	end
	
	if not self.trophy then return end
	
	self.trophy_image = love.graphics.newImage(self.trophy)
	self.trophy_width = 1080
	self.trophy_height = 1080
	self.trophy_scale = 0.2
	self.trophy_x = VIRTUAL_WIDTH / 2 - (self.trophy_width*self.trophy_scale)/2
	self.trophy_y = VIRTUAL_HEIGHT / 2 - (self.trophy_height*self.trophy_scale)/2
	
end

function ScoreState:update(dt)
    -- go back to play if enter is pressed
    if love.keyboard.wasPressed('enter') or love.keyboard.wasPressed('return') then
        gStateMachine:change('countdown')
    end
end

function ScoreState:render()
    -- simply render the score to the middle of the screen
	
	-- Draw trophy in the back
	if self.trophy then
		love.graphics.draw(self.trophy_image, self.trophy_x, self.trophy_y, 0, self.trophy_scale, self.trophy_scale)
	end
	
    love.graphics.setFont(flappyFont)
    love.graphics.printf('Oof! You lost!', 0, 64, VIRTUAL_WIDTH, 'center')

    love.graphics.setFont(mediumFont)
    love.graphics.printf('Score: ' .. tostring(self.score), 0, 100, VIRTUAL_WIDTH, 'center')

    love.graphics.printf('Press Enter to Play Again!', 0, 160, VIRTUAL_WIDTH, 'center')
end