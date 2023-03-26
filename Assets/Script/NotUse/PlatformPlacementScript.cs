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
        //�Q�[���I�u�W�F�N�g�̃|�W�V��������
        pos = transform.position;
        board = new Boad[rowMax, colMax];

        //�ʒu���ʗp
        Initial();

        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                //00�`�c*���̔z�񂩂烉���_���őI��
                int index = Random.Range(0, numbers.Count);

                //�����ɃZ�b�g
                int ransu = numbers[index];

                //������
                if ((ransu / 10) % 2 == 0)
                {
                    if ((ransu % 10) % 2 != 0)
                    {
                        //�����16��ڂł͂Ȃ������ꍇ�����v���b�g�t�H�[����ǉ�
                        if (moveCount != 16)
                        {
                            //�񎟔z��ɃZ�b�g board[(�����ŏo���z��̒��g��2����),(�����ŏo���z��̒��g��1����)]
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
                        //��������16��ڂł͂Ȃ������ꍇ�����v���b�g�t�H�[����ǉ�
                        if (moveCount != 16)
                        {
                            //�񎟔z��ɃZ�b�g board[(�����ŏo���z��̒��g��2����),(�����ŏo���z��̒��g��1����)]
                            board[ransu / 10, ransu % 10] = Boad.MovePrat;
                            moveCount++;
                        }
                        else
                        {
                            board[ransu / 10, ransu % 10] = Boad.Prat;
                        }
                    }
                }
                //���̍s��i���W�j�͏��O
                numbers.RemoveAt(index);
            }
        }

        //�z�u
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
                //���̗�Ɉړ�
                SubXZ(prat);
                pos.x += objX + spaceArea;
            }
            //���̍s�Ɉړ�
            pos.z += objZ + spaceArea;
            //�񃊃Z�b�g
            pos.x = 0;
        }
    }

    void Apper(GameObject o)
    {
        o = Instantiate(prat, transform);
        //�ݒu�ʒu�Ɉړ�
        o.transform.position = pos;
    }

    void ApperMove(GameObject o)
    {
        o = Instantiate(pratM, transform);
        //�ݒu�ʒu�Ɉړ�
        o.transform.position = pos;
    }

    void SubXZ(GameObject o)
    {
        //����i���W�ʒu�ύX�p�j
        objX = o.transform.localScale.x;
        objZ = o.transform.localScale.z;
    }

    //������� 00�`77�̈ꎟ�z��(00,01,02,03,04,05,06,07,10,11,12�`)
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
