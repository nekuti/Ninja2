using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ando
{
    //  スカイボックス用スクリプト(法線を反転させる)
    public class Skybox : MonoBehaviour
    {
        void Start()
        {
            //  メッシュフィルターの取得
            MeshFilter filter = GetComponent<MeshFilter>();
            //  メッシュを保存
            Mesh mesh = filter.mesh;

            //  サブメッシュの数だけ回す
            for (int i = 0; i < mesh.subMeshCount; i++)
            {
                //  ポリゴンのインデックスを取得する
                int[] triangles = mesh.GetTriangles(i);

                //  三角形なので３つずつ足していく
                for (int j = 0; j < triangles.Length; j += 3)
                {
                    //  ポリゴンを反転させる
                    int index = triangles[j + 1];
                    triangles[j + 1] = triangles[j + 2];
                    triangles[j + 2] = index;
                }
                //  反転したメッシュを設定する
                mesh.SetTriangles(triangles, i);
            }
        }
    }
}
