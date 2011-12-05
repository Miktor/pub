
--The animation states to update
animationStates = {}

--Indicates whether the animation states should be updated
animationUpdateEnabled = true

--The speed of the animation playback
animationSpeed = 1

--Called when the scene is activated
function Activating(scene)
    --Some entity animations to retrieve for playback
    --Each item is an array of {entity name, entity animation name}
    local entityAnimations = 
        {
        {'Cylinder01', 'Bend'},
        {'Cylinder02', 'ArmBend'},
        {'Body', 'Springy'},
        {'Body01', 'SlowWalk'},
        {'Box01', 'Box01'}
        }
    
    --Get the entity animation states
    for index, animation in ipairs(entityAnimations) do
        local entityName = animation[1]
        local animationName = animation[2]
        local animationState = scene:GetEntityAnimationState(entityName, animationName)
        if animationState then            
            print('Retrieved entity', entityName, 'animation state', animationName)
            animationState.Enabled = true
            animationState.Looped = true
            animationStates[#animationStates + 1] = animationState
        else    
            print('Failed to get entity', entityName, 'animation state', animationName)
        end
    end    
end

--Called when the scene is deactivated
--Since this implementation doesn't do anything, it could be left out of the script
function Deactivating(scene)    
end

--Called when the scene is updated
function Update(scene, elapsedTime)
    --Toggle animation playback state
    if viewer.InputSystem.Keyboard:PressedKey(viewer.Keys.RETURN) then
        animationUpdateEnabled = not animationUpdateEnabled
    end
    
    --Speed up or slow down the animation
    if viewer.InputSystem.Keyboard:PressedKey(viewer.Keys.LBRACKET) then
        animationSpeed = math.max(.1, animationSpeed - .1)        
    elseif viewer.InputSystem.Keyboard:PressedKey(viewer.Keys.RBRACKET) then
        animationSpeed = animationSpeed + .1        
    end
    
    --Update animations    
    if animationUpdateEnabled then
        for index, animationState in ipairs(animationStates) do
            animationState:AddTime(elapsedTime * animationSpeed)
        end
    end
    
    --Show/hide the walking guy if the '1' key is pressed
    if viewer.InputSystem.Keyboard:PressedKey(viewer.Keys._1) then
        local node = scene:GetSceneNode('Body')
        if node then
            --The 'true' parameter indicates that the visibility change should affect all node children, if any
            node:FlipVisibility(true) 
        end    
    end
end