
StatsMenuState = Class{__includes = BaseState}

function StatsMenuState:init(pokemon, stats, onClose)
    self.pokemon = pokemon
    self.newHP = stats.newHP
    self.newAtt = stats.newAtt
    self.newDef = stats.newDef
    self.newSpd = stats.newSpd

    self.befHP = self.pokemon.HP - self.newHP
    self.befAtt = self.pokemon.attack - self.newAtt
    self.befDef = self.pokemon.defense - self.newDef
    self.befSpd = self.pokemon.speed - self.newSpd

    self.onClose = onClose or function() end
    
    self.statsMenu = Menu {
        x = 0,
        y = VIRTUAL_HEIGHT - 64,
        width = VIRTUAL_WIDTH,
        height = 64,
        showCursor = false,
        font = gFonts['small'],
        items = {
            {
                text = 'HP: ' .. self.befHP .. ' + ' .. self.newHP .. ' = ' .. self.pokemon.HP,
                onSelect = function()
                    self:close()
                end
            },
            {
                text = 'Attack: ' .. self.befAtt .. ' + ' .. self.newAtt .. ' = ' .. self.pokemon.attack,
                onSelect = function()
                    self:close()
                end
            },
            {
                text = 'Defense: ' .. self.befDef .. ' + ' .. self.newDef .. ' = ' .. self.pokemon.defense,
                onSelect = function()
                    self:close()
                end
            },
            {
                text = 'Speed: ' .. self.befSpd .. ' + ' .. self.newSpd .. ' = ' .. self.pokemon.speed,
                onSelect = function()
                    self:close()
                end
            }
        }
    }
end

function StatsMenuState:close()
    gStateStack:pop()
    self.onClose()
end

function StatsMenuState:update(dt)
    self.statsMenu:update(dt)
end

function StatsMenuState:render()
    self.statsMenu:render()
end