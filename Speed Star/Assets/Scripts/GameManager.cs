﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

//K.R
public class GameManager : MonoBehaviour
{
    static GameManager instance;
    protected static readonly string[] findTags = { "GameManager", };

    [SerializeField] GameObject airFenceMark;
    TextMeshProUGUI score;

    /* -- Score (ゲーム中のスコアを入れる) ---------------------------------------------------------- */
    public int _score { set; get; } = 0;

    /* -- TrickCount (ゲーム中のトリックをした回数) ------------------------------------------------ */
    public int trickCount { set; get; } = 0;

    /* -- Time (ゲーム中時間) ------------------------------------------------------------------------ */
    public int _time { set; get; } = 0;

    /* -- Jump入力 ----------------------------------------------------------------------------------- */
    public int jumpKey { get { return _jumpKey; } }
    public int _jumpKey = 0;

    public static GameManager Instance
    {
        get {
            if (instance == null) {
                Type type = typeof(GameManager);

                foreach (var tag in findTags) {
                    GameObject[] objs = GameObject.FindGameObjectsWithTag(tag);

                    for (int j = 0; j < objs.Length; j++) {
                        instance = (GameManager)objs[j].GetComponent(type);
                        if (instance != null) return instance;
                    }
                }

                Debug.LogWarning(string.Format("{0} is not found", type.Name));
            }

            return instance;
        }
    }

    void Awake()
    {
        _score = 0;
        trickCount = 0;
        _time = 0;
        score = FindObjectOfType<TextMeshProUGUI>();
        Player.Create();
    }

    void Update()
    {
        Text();
        Jump();
    }

    void Text()
    {
        score.text = _score.ToString();
    }

    void Jump()
    {
#if UNITY_EDITOR
        if (EventSystem.current.IsPointerOverGameObject()) return;
#else
        if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId)) return;
#endif

        if (Input.GetMouseButtonDown(0)) _jumpKey = 2;      //ジャンプ
    }

    public IEnumerator AirMark()
    {
        for (int i = 0; i <= 2; i++) {
            airFenceMark.SetActive(true);
            yield return new WaitForSeconds(0.3f);
            airFenceMark.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
        yield return null;
    }
}
