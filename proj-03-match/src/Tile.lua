--[[
    GD50
    Match-3 Remake

    -- Tile Class --

    Author: Colton Ogden
    cogden@cs50.harvard.edu

    The individual tiles that make up our game board. Each Tile can have a
    color and a variety, with the varietes adding extra points to the matches.
]]

Tile = Class{}

function Tile:init(x, y, color, variety)
    
    -- board positions
    self.gridX = x
    self.gridY = y

    -- coordinate positions
    self.x = (self.gridX - 1) * 32
    self.y = (self.gridY - 1) * 32

    -- tile appearance/points
    self.color = color
    self.variety = variety
	
	local probability = 5
	self.shiny = (math.random(100) <= probability)
	self.shinyTable = {timer = nil, factor = 0.3}
end

function Tile:render(x, y)
    
    -- draw shadow
    love.graphics.setColor(34, 32, 52, 255)
    love.graphics.draw(gTextures['main'], gFrames['tiles'][self.color][self.variety],
        self.x + x + 2, self.y + y + 2)

    -- draw tile itself
    love.graphics.setColor(255, 255, 255, 255)
    love.graphics.draw(gTextures['main'], gFrames['tiles'][self.color][self.variety],
        self.x + x, self.y + y)
		
	if self.shiny then
		love.graphics.setColor(255, 255, 255, self.shinyTable.factor)
		love.graphics.rectangle('fill', (self.gridX - 1) * 32 + (VIRTUAL_WIDTH - 270),
			(self.gridY - 1) * 32 + 18, 30, 30, 4)
		
		if not self.shinyTable.timer then
                                
			self.shinyTable.timer = Timer.tween(0.5, {[self.shinyTable] = { factor = 0 }})
			:finish(function()
			
				Timer.tween(0.5, {[self.shinyTable] = { factor = 0.3 }})
				:finish(function() 
				
					self.shinyTable.timer = nil
					
				end)
			end)
		end
		
	end
		
end