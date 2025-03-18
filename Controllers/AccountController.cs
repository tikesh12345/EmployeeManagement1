using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using MySql.Data;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using EmployeeManagementSystem.Models;
using static ReturnClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

public class AccountController : Controller
{
    private readonly string _connectionString = "server=localhost;user=root;password=##Tikesh.12345;database=employeemanagementsystem;port=3306;";

    public MySqlDataAdapter? Adapter;
    public MySqlDataReader? reader;
    public MySqlCommand? command;
    public MySqlParameter? objMySqlParameter;
    ReturnDataTable dt;

    [HttpGet]
    public IActionResult Register() => View();

    [HttpPost]
    /*    public IActionResult Register(string username, string email, string password, string role)
        {
            string hashedPassword = HashPassword(password);
            string userId = Guid.NewGuid().ToString();

            using (MySql.Data.MySqlClient.SqlConnection con = new SqlConnection(_connectionString))
            {
                string query = "INSERT INTO Users (Id, UserName, Email, PasswordHash, Role) VALUES (@Id, @UserName, @Email, @Password, @Role)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Id", userId);
                cmd.Parameters.AddWithValue("@UserName", username);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@Role", role);

                con.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Login");
        }
    */

    [HttpPost]
    public async Task<IActionResult> Register(string username, string email, string password, string role)
    {
        string hashedPassword = HashPassword(password);
        string userId = Guid.NewGuid().ToString();
        string query = "INSERT INTO Users (Id, UserName, Email, PasswordHash, Role) VALUES (@Id, @UserName, @Email, @Password, @Role)";

        MySqlParameter[] param = new MySqlParameter[]
        {
                new MySqlParameter("@Id", MySqlDbType.VarChar) {Value=userId},
                new MySqlParameter("@UserName", MySqlDbType.VarChar) {Value=username},
                new MySqlParameter("@Email", MySqlDbType.VarChar) {Value=email},
                new MySqlParameter("@Password", MySqlDbType.VarChar) {Value=hashedPassword},
                new MySqlParameter("@Role", MySqlDbType.VarChar) {Value=role},
         };
        bool result = await executeInsertQuery_async(query, param);
        if (result == true)
        {
        }
        return RedirectToAction("Index","Home");

    }

    [HttpGet]
    public IActionResult Login() => View();

    /*[HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        string hashedPassword = HashPassword(password);
        User user = null;

        *//*        using (SqlConnection con = new SqlConnection(_connectionString))
                {
                    string query = "SELECT Id, UserName, Role FROM Users WHERE Email = @Email AND PasswordHash = @Password";
                    SqlCommand cmd = new SqlCommand(query, con);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@Password", hashedPassword);

                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        user = new User
                        {
                            Id = reader["Id"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
        *//*
        string query = "SELECT Id, UserName, Role FROM Users WHERE Email = @email AND PasswordHash = @password";

        MySqlParameter[] param = new MySqlParameter[]
           {
                new MySqlParameter("@Email", MySqlDbType.VarChar) { Value = email},
                 new MySqlParameter("@Password", MySqlDbType.VarChar) { Value = hashedPassword}

           };
        dt = await executeSelectQuery_async(query, param);
        if (dt.table.Rows.Count > 0)
        {
            user = new User
            {
                Id = dt?.table?.Rows[0]["Id"]?.ToString(),
                UserName = dt?.table?.Rows[0]["UserName"]?.ToString(),
                Role = dt?.table?.Rows[0]["Role"]?.ToString(),

            };
            
        }

        if (user == null)
            return View();

        // Store session & cookie
        HttpContext.Session.SetString("UserId", user?.Id);
        HttpContext.Session.SetString("UserName", user?.UserName);
        HttpContext.Session.SetString("UserRole", user?.Role);
        Response.Cookies.Append("UserName", user.UserName, new CookieOptions { Expires = DateTime.Now.AddDays(7) });

        return RedirectToAction("EmployeeList", "Account");
    }
*/
    // POST: Login
    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        string hashedPassword = HashPassword(password);
        User user = null;

        string query = "SELECT Id, UserName, Role FROM Users WHERE Email = @Email AND PasswordHash = @Password";

        using (MySqlConnection con = new MySqlConnection(_connectionString))
        {
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);

                con.Open();
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        user = new User
                        {
                            Id = reader["Id"].ToString(),
                            UserName = reader["UserName"].ToString(),
                            Role = reader["Role"].ToString()
                        };
                    }
                }
            }
        }

        if (user == null)
        {
            ViewBag.Message = "Invalid email or password";
            return View();
        }

        // 🔹 Create Authentication Cookie
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Role, user.Role)
    };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                      new ClaimsPrincipal(claimsIdentity), authProperties);

        // Store session for extra usage (optional)
        HttpContext.Session.SetString("UserId", user.Id);
        HttpContext.Session.SetString("UserName", user.UserName);
        HttpContext.Session.SetString("UserRole", user.Role);

        return RedirectToAction("EmployeeList","Account");
    }



    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        HttpContext.Session.Clear(); // Clear session data
        return RedirectToAction("Login");
    }


    [Authorize]
    [HttpGet]
    public IActionResult EmployeeList() => View();
    public async Task<ReturnDataTable> executeSelectQuery_async(String _query, MySqlParameter[] sqlParameter)
    {
        ReturnDataTable dt = new ReturnDataTable();
        try
        {
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddRange(sqlParameter);

                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.SelectCommand = cmd;
                        await Adapter.FillAsync(dt.table);
                        dt.status = true;
                    }
                }
            }
        }
        catch (MySqlException ec)
        {
            Gen_Error_Rpt.Write_Error("executeSelectQuery - Query: " + _query + "\n   error - ", ec);
            dt.status = false;
            dt.message = ec.Message;
        }
        catch (Exception e)
        {

        }
        return dt;
    }


    public async Task<bool> executeInsertQuery_async(String _query, MySqlParameter[] sqlParameter)
    {
        bool status = true;
        try
        {
            using (MySqlConnection con = new MySqlConnection(_connectionString))
            {
                using (MySqlCommand cmd = new MySqlCommand())
                {
                    con.Open();
                    cmd.Connection = con;
                    cmd.CommandText = _query;
                    cmd.Parameters.AddRange(sqlParameter);
                    using (Adapter = new MySqlDataAdapter())
                    {
                        Adapter.InsertCommand = cmd;
                        await cmd.ExecuteNonQueryAsync();
                        status = true;
                    }
                }
            }
        }
        catch (MySqlException ex2)
        {
            status = false;
        }
        return status;
    }


    private string HashPassword(string password)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (byte b in bytes)
                builder.Append(b.ToString("x2"));
            return builder.ToString();
        }
    }
}
