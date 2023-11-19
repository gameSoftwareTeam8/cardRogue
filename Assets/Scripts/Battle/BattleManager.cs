using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Inst {  get; private set; }
    void Awake() => Inst = this;
    [SerializeField] NotificationPanel notificationPanel;
 
    void Start()
    {
        Locator.enemy_manager.create_enemy();
        StartGame();
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        InputCheatKey();
#endif
    }

    void InputCheatKey()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
            TurnManager.Inst.ProcessDrawPhase();
        if(Input.GetKeyDown(KeyCode.Keypad2))
            TurnManager.Inst.try_turn_end();
    }

    public void StartGame()
    {
        StartCoroutine(TurnManager.Inst.StartGameCo());
    }

    public void Notification(string message)
    {
        notificationPanel.Show(message);
    }
}
