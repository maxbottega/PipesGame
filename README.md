PipesGame
=========

## FSM Event

Now `GAME_VIEW/_CircuitMaster` is attatched a FSM that receeive all the event below:

1. Level_1_start
1. Level_2_start
1. Level_3_start
1. Level_4_start
1. Level_5_start
1. Level_1_end
1. Level_2_end
1. Level_3_end
1. Level_4_end
1. Level_5_end
1. item_A_work - item_L_work
1. item_A_broken - item_L_broken

## Time record 

`GAME_VIEW/_CircuitMaster.timeRecord` asign to a NGUI label , to show what time it has gone .

## Game Flow

1. open scene at Assets/Scenes/Game_Core.unity
1. Start Game
1. NGUI : loading
1. NGUI : choose singleplay or multiplay mode
1. NGUI : acount and password
1. `GAME_VIEW` acitvated
1. `GAME_VIEW/Level 1` acitvated 
1. When `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump` is set in right *Container* , `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump.Element.workActivate` is acitvated ( firework particle
1. When `GAME_VIEW/Level 1/DYNAMIC_ITEMS/A_Pump` is set in other *Container* , it wiil be no effect. 
1. When `GAME_VIEW/_CircuitMaster.Circuit.isAllWork` is true, `GAME_VIEW/Level 1.CircuitSet.successActivate` is acitivated ( firework particle in the millde of screen
1. deactivate `GAME_VIEW/Level 1` , and activae `GAME_VIEW/Level 2`
1. so on the level 2 - 4 .
1. When `GAME_VIEW/_CircuitMaster.Circuit.isAllWork` is true in *Level 4* , it will go nowhere any more .
1. the end
