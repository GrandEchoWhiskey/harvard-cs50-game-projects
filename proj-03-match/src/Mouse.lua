Mouse = Class{}

function Mouse:init()
	self.pressed = 0
	self.tile = 32
end

function Mouse:firstClick()

	-- When relased set pressed to false
	if not love.mouse.isDown(1) then
		self.pressed = 0
	end
	
	-- When not pressed and mouse button down, change pressed to true and return
	if self.pressed == 0 and love.mouse.isDown(1) then
		self.pressed = 1
		return true
	end
	
	-- Mouse not clicked or never relased
	return false
	
end

function Mouse:hover(board)

	local mouse_x, mouse_y = push:toGame(love.mouse.getX(), love.mouse.getY())
	
	for x = 1, 8 do
		for y = 1, 8 do
		
			local minX, maxX = self:_getX_MinMax(board, x, y)
			local minY, maxY = self:_getY_MinMax(board, x, y)
			
			local pass = self:_isInRange(minX, maxX, mouse_x)
			pass = pass and self:_isInRange(minY, maxY, mouse_y)
			
			if pass then return x - 1, y - 1 end
			
		end
	end

end

function Mouse:isHover(board)

	local mouse_x, mouse_y = push:toGame(love.mouse.getX(), love.mouse.getY())
	
	for x = 1, 8 do
		for y = 1, 8 do
		
			local minX, maxX = self:_getX_MinMax(board, x, y)
			local minY, maxY = self:_getY_MinMax(board, x, y)
			
			local pass = self:_isInRange(minX, maxX, mouse_x)
			pass = pass and self:_isInRange(minY, maxY, mouse_y)
			
			if pass then return true end
			
		end
	end
	
	return false
	
end

function Mouse:_getX_MinMax(board, x, y)

	local min = board.tiles[y][x].x + board.x
	local max = min + self.tile
	
	return min, max
	
end

function Mouse:_getY_MinMax(board, x, y)

	local min = board.tiles[y][x].y + board.y
	local max = min + self.tile
	
	return min, max
	
end

function Mouse:_isInRange(min, max, point)
	
	return (point >= min and point <= max)
	
end