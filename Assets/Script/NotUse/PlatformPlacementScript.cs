using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPlacementScript : MonoBehaviour
{
    enum Boad
    {
        None,
        Prat,
        MovePrat
    }

    public GameObject prat;
    public GameObject pratM;
    public int rowMax;
    public int colMax;
    public float spaceArea = 0;
    public int endMovePratNum = 16;

    float objX, objZ;
    Boad[,] board;
    Vector3 pos;

    List<int> numbers = new List<int>();
    int moveCount;

    // Start is called before the first frame update
    void Start()
    {
        //ゲームオブジェクトのポジションを代入
        pos = transform.position;
        board = new Boad[rowMax, colMax];

        //位置判別用
        Initial();

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                //00～縦*横の配列からランダムで選択
                int index = Random.Range(0, numbers.Count);

                //乱数にセット
                int ransu = numbers[index];

                //偶数列
                if ((ransu / 10) % 2 == 0)
                {
                    if ((ransu % 10) % 2 != 0)
                    {
                        //奇数且つ16回目ではなかった場合動くプラットフォームを追加
                        if (moveCount != 16)
                        {
                            //二次配列にセット board[(乱数で出た配列の中身の2桁目),(乱数で出た配列の中身の1桁目)]
                            board[ransu / 10, ransu % 10] = Boad.MovePrat;
                            moveCount++;
                        }
                        else
                        {
                            board[ransu / 10, ransu % 10] = Boad.Prat;
                        }
                    }
                }
                else
                {
                    if ((ransu % 10) % 2 == 0)
                    {
                        //偶数且つ16回目ではなかった場合動くプラットフォームを追加
                        if (moveCount != 16)
                        {
                            //二次配列にセット board[(乱数で出た配列の中身の2桁目),(乱数で出た配列の中身の1桁目)]
                            board[ransu / 10, ransu % 10] = Boad.MovePrat;
                            moveCount++;
                        }
                        else
                        {
                            board[ransu / 10, ransu % 10] = Boad.Prat;
                        }
                    }
                }
                //その行列（座標）は除外
                numbers.RemoveAt(index);
            }
        }

        //配置
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                switch (board[i, j])
                {
                    case Boad.Prat:
                        Apper(prat);
                        break;
                    case Boad.MovePrat:
                        ApperMove(pratM);
                        break;
                    default: break;
                }
                //次の列に移動
                SubXZ(prat);
                pos.x += objX + spaceArea;
            }
            //次の行に移動
            pos.z += objZ + spaceArea;
            //列リセット
            pos.x = 0;
        }
    }

    void Apper(GameObject o)
    {
        o = Instantiate(prat, transform);
        //設置位置に移動
        o.transform.position = pos;
    }

    void ApperMove(GameObject o)
    {
        o = Instantiate(pratM, transform);
        //設置位置に移動
        o.transform.position = pos;
    }

    void SubXZ(GameObject o)
    {
        //代入（座標位置変更用）
        objX = o.transform.localScale.x;
        objZ = o.transform.localScale.z;
    }

    //初期状態 00～77の一次配列(00,01,02,03,04,05,06,07,10,11,12～)
    void Initial()
    {
        for (int i = 0; i < board.GetLength(0) * 10; i += 10)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                numbers.Add(i + j);
            }
        }
    }
}
