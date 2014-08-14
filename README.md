##Dataeditor.Arce##
--------------------------------

###Summary###
```Dataeditor.Arce``` is a project which hoping to provide a more efficient and unlimitted **database inteface** for users in *RPG Maker Series*.

```Dataeditor.Ruby```is a project of *DataEditor.Arce* which using ```IronRuby``` to describe the user interface. You can easily change, add or remove ruby scripts, to change the appearance of the program.

--------------------------------
###Progress###
We released a 0.0.1 version now. Folloing is what it can do now :

*   Open *RPG Maker XP* projects. 
    * We can open and edit *RPG Maker VX* or *RPG Maker VX Ace* data now, but we need time to adjust the user interface.
*   Edit almost all data in **database** exist *Animation* and *Tileset*, *Event* is now readonly.
*   Adjusted some control from RM, which make it more convenient.
*   Provide another editor like ```regedit``` which can easily open and change data in any Ruby format.
*   Automatically backup the data.
*   A *Change* window, help you change numbers of data.
*   Limitted *undo* and *redo*.
*   Limitted *taint flag*.

--------------------------------
###IronRuby API###
```Dataeditor.Ruby``` runs on *IronRuby 1.1*, a Builder class is added to environment to help build the User Interface: 

    # Add an control, whose type decided by flag and run as arguments to the control.
    Builder.Add(:flag, arguments [, &block]) 
    
    # Get into the control, to describe the inner structure of control.
    Builder.In(control)
    
    # Pop out from the nearest control added.
    Builder.Out
    
    # Space some pixels.
    Builder.Space(px)
    Builder.Space(x, y)
    
    # Change the order control puts.
    Builder.Order
    Builder.Order(type)
    
    # Keep the Order, and move to next line or row.
    Builder.Next
 
--------------------------------
###Warning###

+ Using a self-programmed I/O module, we are not very concern that all data can safely saved. **Don't use it in working environment**, at least now.
+ Although what you seen at this page is all English, the program is ALL IN **CHINESE**. That may keep for a long time.
+ If you smiled for last item, **Put the program in a totally English path**, or it will break down on loading, and you have to kill it in *WIndows Task Manager*.

 --------------------------------
###Welcome###
We need more people now. Join us!!
