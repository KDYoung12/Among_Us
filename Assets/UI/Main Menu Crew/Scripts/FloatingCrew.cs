﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingCrew : MonoBehaviour
{
    // 크루원의 색상을 저장하는 변수
    public EPlayerColor playerColor;

    // 떠다니는 크루원의 색깔과 형태 지정
    private SpriteRenderer spriteRenderer;
    // 크루원이 나타내는 방향
    private Vector3 direction;
    // 날아가는 속도와 회전값
    private float floatingSpeed;
    private float rotateSpeed;

    private void Awake()
    {
        // 프로퍼티에 저장 (초기화)
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetFloatingCrew(Sprite sprite, EPlayerColor playerColor, Vector3 direction, float floatingSpeed,
        float rotateSpeed, float size)
    {
        // 색깔, 이동 방향, 이동 속도, 회전 값은 내부 프로퍼티에 저장
        this.playerColor = playerColor;
        this.direction = direction;
        this.floatingSpeed = floatingSpeed;
        this.rotateSpeed = rotateSpeed;

        // sprite로 이미지, 색상 변경
        spriteRenderer.sprite = sprite;
        spriteRenderer.material.SetColor("_PlayerColor", PlayerColor.GetColor(playerColor));

        // size 매개변수를 이용하여 크루원의 사이즈 변경
        transform.localScale = new Vector3(size, size, size);
        // sortingOrder로 크기가 작은 크루원은 뒤로 가고 크기가 큰 크루원은 앞으로
        spriteRenderer.sortingOrder = (int)Mathf.Lerp(1, 32767, size);
    }

    void Update()
    {
        // 크루원 이동, 회전
        transform.position += direction * floatingSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0f, 0f, rotateSpeed));
    }
}
