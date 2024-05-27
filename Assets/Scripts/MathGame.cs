using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MathGame : MonoBehaviour
{

    // public Button[] numbers;
    // public Button check;
    public TMP_Text[] problem;
    public TMP_Text scoretext;
    public GameObject gameoverscreen;
    public Button tryagain;
    public Image timerBar;
    public Image bg;

    public string type;
    public string sign;
    public int num1;
    public int num2;
    public int answer;
    public int playeranswer;
    public string playerinput;

    public int score;
    public float timer;
    public float maxtime;

    // Start is called before the first frame update
    void Start()
    {
        // int i = 0;
        // foreach (Button btn in numbers) {
        //     btn.onClick.AddListener(Typing);
        //     i++;
        // }
        // check.onClick.AddListener(CheckAns);

        maxtime = 15f;
        timer = 15f;
        score = 0;
        bg = GameObject.Find("Panel").GetComponent<Image>();
        timerBar = GameObject.Find("Timer").GetComponent<Image>();
        tryagain.onClick.AddListener(Again);
        gameoverscreen.SetActive(false);

        RerollType();
        if (type == "Addition") {
            RerollProblemAdd();
        }
        if (type == "Subtraction") {
            RerollProblemSub();
        }
        if (type == "Multiplication") {
            RerollProblemMul();
        }
        if (type == "Division") {
            RerollProblemDiv();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // display num1 num2 and ans --> sign is updated when type is
        problem[0].text = "" + num1;
        problem[2].text = "" + num2;
        problem[3].text = playerinput;
        
        // update timer
        if (timer > 0f) {
            timer -= Time.deltaTime;
            timerBar.fillAmount = timer / maxtime;
        } else {
            gameoverscreen.SetActive(true);
            Time.timeScale = 0;
        }

        
        // player input answer
        foreach (char c in Input.inputString) {
            int tempInt;
            if (c == '\b') { // backspace/delete
                if (playerinput.Length != 0) {
                    playerinput = playerinput.Substring(0, playerinput.Length - 1);
                }
            } else if (c == '\n' || c == '\r') { // enter / return
                CheckAns();
            } else if (int.TryParse(c.ToString(), out tempInt)) { // only numbers are added
                playerinput += c;
            }
        }
    }

    void CheckAns() {
        playeranswer = int.Parse(playerinput);
        playerinput = "";
        if (playeranswer != answer) {
            Time.timeScale = 0;
            gameoverscreen.SetActive(true);
        } else {
            score++;
            scoretext.text = "Score: " + score;
            // every 10 problems, change the type
            if (score % 10 == 0) {
                RerollType();
            }
            // generate a new problem based on type
            if (type == "Addition") {
                RerollProblemAdd();
            }
            if (type == "Subtraction") {
                RerollProblemSub();
            }
            if (type == "Multiplication") {
                RerollProblemMul();
            }
            if (type == "Division") {
                RerollProblemDiv();
            }
            // reset timer
            timer = 15f;
        }
    }

    void RerollType() {
        // rand index 0-3 to determine type
        int typeindex = Random.Range(0, 4);
        // update type, sign, and backdrop color
        if (typeindex == 0) {
            type = "Addition";
            sign = "+";
            bg.color = new Color32(255, 92, 99, 255);
        }
        if (typeindex == 1) {
            type = "Subtraction";
            sign = "-";
            bg.color = new Color32(255, 250, 92, 255);
        }
        if (typeindex == 2) {
            type = "Multiplication";
            sign = "×";
            bg.color = new Color32(122, 255, 92, 255);
        }
        if (typeindex == 3) {
            type = "Division";
            sign = "÷";
            bg.color = new Color32(92, 160, 255, 255);
        }
        problem[1].text = sign;
    }

    void RerollProblemAdd() {
        num1 = Random.Range(1, 100);
        num2 = Random.Range(1, 100);
        answer = num1 + num2;

        if (answer > 99) {
            RerollProblemAdd();
        }

    }

    void RerollProblemSub() {
        answer = Random.Range(1, 100);
        num2 = Random.Range(1, 100);
        num1 = num2 + answer;
        
        if (num1 > 99) {
            RerollProblemSub();
        }
    }

    void RerollProblemMul() {
        num1 = Random.Range(2, 100);
        num2 = Random.Range(2, 10);
        answer = num1 * num2;

        if (answer > 999) {
            RerollProblemMul();
        }
    }

    void RerollProblemDiv() {
        answer = Random.Range(1, 100);
        num2 = Random.Range(2, 10);
        num1 = num2 * answer;

        if (num1 > 999) {
            RerollProblemDiv();
        }
    }

    void Again() {
        SceneManager.LoadScene("Gameplay");
    }
}
