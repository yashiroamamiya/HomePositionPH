using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IllusionPlugin;
using UnityEngine;
using System.IO;
using Studio;

namespace HomePosition
{
    // IEnhancedPlugin　を継承する
    public class Class1 : IEnhancedPlugin
    {
        // ここらへんの空のメソッドは継承の都合用意する必要がある
        public void OnLevelWasInitialized(int level)
        {
        }

        // pluginが働く対象のexeファイルの名前を指定する。
        public string[] Filter
        {
            get
            {
                string[] array = { "PlayHomeStudio32bit", "PlayHomeStudio64bit" };
                return array;
            }
        }

        // プラグインの名前。デバッグログぐらいにしか出ない。
        public string Name
        {
            get
            {
                return "MultiAngleRotationUnitPH";
            }
        }

        // バージョン。デバッグログ（略）
        public string Version
        {
            get
            {
                return "1.0.0.0";
            }
        }

        public void OnApplicationQuit()
        {

        }

        public void OnFixedUpdate()
        {
        }

        public void OnLateUpdate()
        {
        }

        public void OnApplicationStart()
        {

        }

        public void OnLevelWasLoaded(int level)
        {
        }

        // 画面再描画イベントみたいなもの。
        // たまに呼び出される。
        public void OnUpdate()
        {
            // キー状態の確認。
            // キーイベントみたいなものにハンドラを登録する訳ではないので、
            // なんかしたい人はとにかくOnUpdateで処理を書く。
            if (Input.GetKeyDown(KeyCode.F6))
            {
                Boolean isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                if (isShift)
                {
                    // CameraControlクラスはIllusionさん作成のクラス。
                    // カメラの制御に関する各種制御を持っているのでこれを利用してカメラの制御をする。
                    // unityの世界のメソッドだけで処理しようとするとうまくいかない事があるのでがんばれ！
                    // CameraやVectory3dはunityのものです。
                    // illusionのものとunityのものがごっちゃごちゃになっています。
                    // Camera.mainはメインカメラです。これはUnityの機能で提供されています。
                    Studio.CameraControl component2 = Camera.main.GetComponent<Studio.CameraControl>();
                    component2.cameraAngle += new Vector3(0, 0, 270);
                }
                else
                {
                    Studio.CameraControl component2 = Camera.main.GetComponent<Studio.CameraControl>();
                    component2.cameraAngle += new Vector3(0, 0, 90);


                }
            }

            if (Input.GetKeyDown(KeyCode.F7))
            {
                Boolean isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                Studio.CameraControl component = Camera.main.GetComponent<Studio.CameraControl>();
                if (isShift)
                {
                    component.SetCamera(new Vector3(0, getHeight(), 0), Vector3.zero, Quaternion.identity, Vector3.zero);

                    Studio.CameraControl component2 = Camera.main.GetComponent<Studio.CameraControl>();
                    component2.SetCamera(new Vector3(0, getHeight(), 0), new Vector3(0, 0, 0), Quaternion.identity, new Vector3(0, 0, (-1) * getDistance()));
                    component2.transform.Rotate(new Vector3(0, 0, 0));
                    component2.targetPos = new Vector3(0, getHeight(), 0);
                }
                else
                {
                    component.SetCamera(new Vector3(0, getHeight(), 0), Vector3.zero, Quaternion.identity, Vector3.zero);

                    Studio.CameraControl component2 = Camera.main.GetComponent<Studio.CameraControl>();
                    component2.SetCamera(new Vector3(0, getHeight(), 0), new Vector3(0, 180, 0), Quaternion.identity, new Vector3(0, 0, (-1) * getDistance()));
                    component2.transform.Rotate(new Vector3(0, 180, 0));
                    component2.targetPos = new Vector3(0, getHeight(), 0);
                }
            }
            if (Input.GetKeyDown(KeyCode.F8))
            {
                Boolean isShift = Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
                try
                {
                    Studio.CameraControl component = Camera.main.GetComponent<Studio.CameraControl>();
                    if (isShift)
                    {
                        component.SetCamera(new Vector3(0, getHeight(), 0), Vector3.zero, Quaternion.identity, Vector3.zero);

                        Studio.CameraControl component2 = Camera.main.GetComponent<Studio.CameraControl>();
                        component2.SetCamera(new Vector3(0, getHeight(), 0), new Vector3(0, 90, 0), Quaternion.identity, new Vector3(0, 0, (-1) * getDistance()));
                        component2.transform.Rotate(new Vector3(0, 90, 0));
                        component2.targetPos = new Vector3(0, getHeight(), 0);
                    }
                    else
                    {
                        component.SetCamera(new Vector3(0, getHeight(), 0), Vector3.zero, Quaternion.identity, Vector3.zero);

                        Studio.CameraControl component2 = Camera.main.GetComponent<Studio.CameraControl>();
                        component2.SetCamera(new Vector3(0, getHeight(), 0), new Vector3(0, 270, 0), Quaternion.identity, new Vector3(0, 0, (-1) * getDistance()));
                        component2.transform.Rotate(new Vector3(0, 270, 0));
                        component2.targetPos = new Vector3(0, getHeight(), 0);
                    }
                }
                catch (Exception e)
                {

                }
            }

        }

        public float getDistance()
        {
            // 設定ファイルから値を読みだすには、userdata/ModPerfs.iniに書いた上で下記を使用する。
            String distance = ModPrefs.GetString("MARU", "distance", "5");

            float ret = 5.0f;

            try
            {
                ret = float.Parse(distance);
            }
            catch(Exception e)
            {

            }
            return ret;
        }

        public float getHeight()
        {
            String height = ModPrefs.GetString("MARUNEO", "height", "0.8");

            float ret = 0.8f;

            try
            {
                ret = float.Parse(height);
            }
            catch (Exception e)
            {

            }
            return ret;
        }

        public void logSave(string txt)
        {
            StreamWriter sw;
            FileInfo fi;
            fi = new FileInfo(Application.dataPath + "/errorLog.txt");
            sw = fi.AppendText();
            sw.WriteLine(txt);
            sw.Flush();
            sw.Close();
        }


        // Token: 0x060031F8 RID: 12792 RVA: 0x0017DB70 File Offset: 0x0017BD70
        private ObjectCtrlInfo TryGetLoop(TreeNodeObject _node)
        {
            if (_node == null)
            {
                return null;
            }
            ObjectCtrlInfo result = null;
            if (Singleton<Studio.Studio>.Instance.dicInfo.TryGetValue(_node, out result))
            {
                return result;
            }
            return this.TryGetLoop(_node.parent);
        }

    }
}
