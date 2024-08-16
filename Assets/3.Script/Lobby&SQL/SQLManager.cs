using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;
using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.IO;
using System.Text;

// Table 데이터 자료형으로 만들어 놓기
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
    public bool isLogin; // 현재 로그인 상태인가?
    public User_info info; // 로그인 중인 유저의 정보
    public MySqlConnection connection; // 연결
    public MySqlDataReader reader; // 데이터를 직접적으로 읽어오는 녀석
    [SerializeField] private string DB_Path = string.Empty;

    public static SQLManager instance = null;
    private void Awake()
    {
        // 1. 싱글톤 적용
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
        // 2. 데이터베이스 연결하기
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
        catch (Exception e) { Debug.Log(e.Message); }
        Debug.Log(DB_Path);
    }

    private string Server_set(string path)
    {
        // config.json의 내용으로 AWS DB 서버 연결
        if (!File.Exists(path))
        {
            Directory.CreateDirectory(path);
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
        // 서버 연결 확인
        if (con.State != System.Data.ConnectionState.Open)
        {
            con.Open();
            if (con.State != System.Data.ConnectionState.Open) { return false; }
        }
        return true;
    }



    // 회원가입(Insert)
    // 빈 칸, 중복검사 통과 시 호출되어야 함
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

    // 정보수정(Update)
    // 빈 칸, 중복검사, 길이 체크 통과 시 호출되어야 함
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
            Update_info();
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
        }
    }

    // 로그인
    // true : 로그인 완료(info에 정보 캐시) / false : 실패(없는 ID 또는 PW 틀림)
    public bool Login(string id, string pw)
    {
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


    // 중복체크 용 메소드
    // return 0 : OK / 1 : 중복 / 2 : 오류
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

    // 게임 완료 시
    public void Result(bool isWin, int score)
    {
        try
        {
            if (!Connection_check(connection)) return;
            string updateColumn = isWin ? "Wins" : "Loses";
            int updatedValue = isWin ? info.wins + 1 : info.loses + 1;

            string SQL_Command = $@"UPDATE User_info
                         SET {updateColumn} = {updatedValue}, HighScore = {Mathf.Max(info.highscore, score)}
                         WHERE User_ID = '{info.User_id}';";
            MySqlCommand cmd = new MySqlCommand(SQL_Command, connection);
            reader = cmd.ExecuteReader();
            if (!reader.IsClosed) reader.Close();
            Update_info();
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
        }
    }

    // 게임 완료 후 User_info 갱신
    public void Update_info()
    {
        try
        {
            if (!Connection_check(connection)) return;
            string SQL_Command = String.Format($@"SELECT User_ID, User_PW, Nickname, Wins, Loses, HighScore
                                                  FROM user_info 
                                                  WHERE User_ID='{info.User_id}';");
            MySqlCommand cmd = new MySqlCommand(SQL_Command, connection);
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                info = new User_info((string)reader["User_ID"], (string)reader["User_PW"], (string)reader["Nickname"],
                                 Convert.ToInt32(reader["Wins"]), Convert.ToInt32(reader["Loses"]), Convert.ToInt32(reader["HighScore"]));
                break;
            }
            if (!reader.IsClosed) reader.Close();
        }
        catch (Exception e)
        {
            if (!reader.IsClosed) reader.Close();
            Debug.Log(e.Message);
        }
    }

}
