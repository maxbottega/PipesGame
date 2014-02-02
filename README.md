PipesGame
=========

## Game Flow 

update at 2014-02-02.

1. open scene `Assets/Scenes/Game_Core.unity`
1. Start Game
1. NGUI : loading
1. NGUI : choose singleplay or multiplay mode
1. NGUI : acount and password
1. `GAME_VIEW` acitvated
1. `GAME_VIEW/Level 1` acitvated 
1. When `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump` is set in right *Container* , `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump.Element.workActivate` is acitvated ( firework particle
1. When `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump` is set in other *Container* , it wiil be no effect. 
1. When `GAME_VIEW/_CircuitMaster.isAllWork` is true, `GAME_VIEW/Level 1/DYNAMIC_ITEMS/Circuit Succeed` is acitivated ( firework particle in the millde of screen
1. deactivate *GAME_VIEW/Level 1* , and activae *GAME_VIEW/Level 2*
1. so on the level 2 - 4 .
1. When `GAME_VIEW/_CircuitMaster.isAllWork` is true in *Level 4* , it will go nowhere any more .
1. the end