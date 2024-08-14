using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Text;

// Table ������ �ڷ������� ����� ����
public class User_info
{
    public string User_id { get; private set; }
    public string User_password { get; private set; }
    public string User_nickname { get; private set; }
    public int wins { get; private set; }
    public int loses { get; private set; }
    public int highscore { get; private set; }

    public User_info(string id, string pw, string nick, int win, int lose, int high)
    {
        User_id = id;
        User_password = pw;
        User_nickname = nick;
        wins = win;
        loses = lose;
        highscore = high;
    }
}

public class SQLManager : MonoBehaviour
{
    public User_info info;
    public MySqlConnection connection; // ����
    public MySqlDataReader reader; // �����͸� ���������� �о���� �༮
    [SerializeField] private string DB_Path = string.Empty;

    public bool isLogin;
    public string EC2publicIpAddress = string.Empty;

    public static SQLManager instance = null;

    private void Awake()
    {
        // 1. �̱��� ����
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        isLogin = false;
        // 2. �����ͺ��̽� �����ϱ�
        DB_Path = Application.dataPath + "/Database";
        string serverinfo = Server_set(DB_Path);
        try
        {
            if (serverinfo.Equals(string.Empty))
            {
                Debug.Log("server info is null");
                return;
            }
            connection = new MySqlConnection(serverinfo);
            connection.Open();
            Debug.Log("DB Open connection complete");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        Debug.Log(DB_Path);
    }

    private string Server_set(string path)
    {
        // Json ���� �ִٸ� Ǯ� ����
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path); // ��� ����
        }
        string JsonString = File.ReadAllText(path + "/config.json");
        JsonData itemdata = JsonMapper.ToObject(JsonString);
        string serverInfo =
            $"Server = {itemdata[0]["IP"]};" +
            $"Database = {itemdata[0]["TableName"]};" +
            $"Uid = {itemdata[0]["ID"]};" +
            $"Pwd = {itemdata[0]["PW"]};" +
            $"Port = {itemdata[0]["PORT"]};" +
            "CharSet=utf8;";
        return serverInfo;
    }

    private bool Connection_check(MySqlConnection con)
    {
        if (con.State != System.Data.ConnectionState.Open)
        {
            con.Open();
            if (con.State != System.Data.ConnectionState.Open)
            {
                return false;
            }
        }
        return true;
    }



    // ȸ������(Insert)
    // �� ĭ, �ߺ��˻�, ���� üũ ��� �� ȣ��Ǿ�� ��
    public void Register(string id, string pw, string nick)
    {
        try
        {
            if (!Connection_check(connection)) return;
            string SQL_Command = String.Format($@"INSERT INTO User_info VALUES('{id}','{pw}','{nick}', 0, 0, 0);");
            MySqlCommand cmd = new MySqlCommand(SQL_Command, connection);
            reader = cmd.ExecuteReader();
            if (!reader.IsClosed) reader.Close();
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
        }
    }

    // ��������(Update)
    // �� ĭ, �ߺ��˻�, ���� üũ ��� �� ȣ��Ǿ�� ��
    public void User_Edit(string id, string pw, string nick)
    {
        try
        {
            if (!Connection_check(connection)) return;
            string SQL_Command = String.Format($@"Update User_info 
                                                  Set User_ID = '{id}', User_PW = '{pw}', Nickname = '{nick}'
                                                  Where User_ID = '{info.User_id}';");
            MySqlCommand cmd = new MySqlCommand(SQL_Command, connection);
            reader = cmd.ExecuteReader();
            if (!reader.IsClosed) reader.Close();
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
        }
    }

    // �α���
    // true : �α��� �Ϸ�(info�� ���� ĳ��) / false : ����(���� ID �Ǵ� PW Ʋ��)
    public bool Login(string id, string pw)
    {
        // ���������� DB���� ������ �������� �޼ҵ�
        // ��ȸ�Ǵ� ������ ���ٸ� false
        // �ִٸ� true���� ������ ���ǹ��� id,pw ����. ������ ĳ���� info�� ���� ���� ����.
        // 1. connection ���� Ȯ�� -> open
        // 2. Reader�� ���� Ȯ�� -> �а� �ִ� ��Ȳ
        // 3. �����͸� �� �о����� -> Reader�� Close ���·� 
        try
        {
            if (!Connection_check(connection)) return false;
            string SQL_Command = String.Format($@"SELECT User_ID, User_PW, Nickname, Wins, Loses, HighScore
                                                  FROM user_info 
                                                  WHERE User_ID='{id}' AND User_PW='{pw}';");
            MySqlCommand cmd = new MySqlCommand(SQL_Command, connection);
            reader = cmd.ExecuteReader();


            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    info = new User_info((string)reader["User_ID"], (string)reader["User_PW"], (string)reader["Nickname"],
                                     Convert.ToInt32(reader["Wins"]), Convert.ToInt32(reader["Loses"]), Convert.ToInt32(reader["HighScore"]));
                    break;
                }
                if (!reader.IsClosed) reader.Close();
                isLogin = true;
                return true;
            }

            if (!reader.IsClosed) reader.Close();
            return false;
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
            return false;
        }
    }


    // �ߺ�üũ �� �޼ҵ�
    // return 0 : OK / 1 : �ߺ� / 2 : ����
    public int Isduplicate(bool isID, string s)
    {
        try
        {
            if (!Connection_check(connection)) return 2;
            string SQL_Command = string.Empty;
            if (isID)
            {
                SQL_Command = String.Format($@"SELECT Wins FROM user_info WHERE User_ID='{s}';");
            }
            else
            {
                SQL_Command = String.Format($@"SELECT Wins FROM user_info WHERE Nickname='{s}';");
            }
            MySqlCommand cmd = new MySqlCommand(SQL_Command, connection);
            reader = cmd.ExecuteReader();

            if(reader.HasRows)
            {
                if (!reader.IsClosed) reader.Close();
                return 1;
            }
            if (!reader.IsClosed) reader.Close();
            return 0;

        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
            return 2;
        }
    }

    // ���� �Ϸ� ��
    public void Result(bool isWin, int score)
    {
        try
        {
            if (!Connection_check(connection)) return;
            string SQL_Command = string.Empty;
            if (isWin)
            {
                SQL_Command = String.Format($@"Update User_info 
                                                  Set Wins = {info.wins + 1}, HighScore = {Mathf.Max(info.highscore, score)}
                                                  Where User_ID = '{info.User_id}';");
            }
            else
            {
                SQL_Command = String.Format($@"Update User_info 
                                                  Set Loses = {info.loses + 1}, HighScore = {Mathf.Max(info.highscore, score)}
                                                  Where User_ID = '{info.User_id}';");
            }
            MySqlCommand cmd = new MySqlCommand(SQL_Command, connection);
            reader = cmd.ExecuteReader();
            if (!reader.IsClosed) reader.Close();
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
        }
        finally
        {
            Update_info();
        }
    }

    // ���� �Ϸ� �� User_info ����
    public bool Update_info()
    {
        try
        {
            if (!Connection_check(connection)) return false;
            string SQL_Command = String.Format($@"SELECT User_ID, User_PW, Nickname, Wins, Loses, HighScore
                                                  FROM user_info 
                                                  WHERE User_ID='{info.User_id}';");
            MySqlCommand cmd = new MySqlCommand(SQL_Command, connection);
            reader = cmd.ExecuteReader();
            info = new User_info((string)reader["User_ID"], (string)reader["User_PW"], (string)reader["Nickname"],
                                     (int)reader["Wins"], (int)reader["Loses"], (int)reader["HighScore"]);
            if (!reader.IsClosed) reader.Close();
            return true;
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
            return false;
        }
    }

}
