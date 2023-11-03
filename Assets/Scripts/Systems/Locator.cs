using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 전역 변수를 참조하기 위한 정적 클래스     <br/>
///                                         <br/>
/// e.g.)                                   <br/>
/// IBoard current_board = Locator.board;
/// </summary>
public static class Locator
{
    /// <summary> 현재 보드 </summary>
    public static IBoard board = null;
    public static CardFactory card_factory = new();
    public static ICardPool card_pool = new CardPool();
    public static VFXFactory vfx_factory = new();
    public static IActionQueue action_queue = new ActionQueue();
    public static IPlayer player = new Player();
    public static EnemyManager enemy_manager = null;
}