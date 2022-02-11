using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace tools
{
    public partial class Form1 : Form
    {
        Panel _pnlMainPage;
        Panel _pnlConnectPage;
        Panel _pnlDBPage;

        Button _btnConnect;
        Button _btnDb;
        Button _btnCreateDB;
        Button _btnDeleteDB;
        Button _btnBack;
        Button _btnBackDb;
        Button _btnCreateTable;
        Button _btnEstablishConnection;

        Label _lblServer;
        Label _lblUser;
        Label _lblPwd;
        Label _lblDB;
        Label _lbDBName;
        Label _lblTableName;
        Label _lblSize;
        Label _lblType;
        Label _lblTitle;

        TextBox _tbServer;
        TextBox _tbDB;
        TextBox _tbUsername;
        TextBox _tbPwd;
        TextBox _tbTableName;
        TextBox _tbTableSize;
        TextBox _tbTableType;
        


        Size btnSize = new Size(80, 30);
        MySqlConnection _connection;
        public Form1()
        {
            InitializeComponent();
            this.Size = new Size(500, 500);
            this.Text = "MySql Tools";


            #region MainPage
            _pnlMainPage = new Panel
            {
                Size = this.Size,
            };

            _lblTitle = new Label
            {
                Text = "Gestion MySql",
                Font = new Font("Helvetica", 15, FontStyle.Regular),
                Size = new Size(this.ClientSize.Width, 30),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0,0)
            };

            _btnConnect = new Button
            {
                Text = "Se connecter",
                TextAlign = ContentAlignment.MiddleCenter,
                Size = btnSize,
                Location = new Point((this.ClientSize.Width - btnSize.Width) /2, (this.ClientSize.Height - btnSize.Height)/2)
            };
            _btnConnect.Click += new EventHandler(BtnConnect_Click);

            _btnDb = new Button
            {
                Text = "DB",
                TextAlign = ContentAlignment.MiddleCenter,
                Size = btnSize,
                Location = new Point(10,10),
                //Enabled = false
            };
            _btnDb.Click += new EventHandler(BtnDB_Click);

            _pnlMainPage.Controls.AddRange(new Control[] { _btnConnect, _btnDb, _lblTitle});
            this.Controls.Add(_pnlMainPage);
            #endregion

            #region ConnectPage
            _pnlConnectPage = new Panel
            {
                Size = _pnlMainPage.Size
            };

            _tbServer = new TextBox
            {
                Width = this.ClientSize.Width - 150
            };
            _tbServer.Location = new Point((this.ClientSize.Width - _tbServer.Width) / 2, this.Top + 30);

            _lblServer = new Label
            {
                Text = "Server:",
                Height = _tbServer.Height
            };
            _lblServer.Location = new Point(_tbServer.Left, _tbServer.Top - _lblServer.Height);

            _tbUsername = new TextBox
            {
                Width = _tbServer.Width
            };
            _tbUsername.Location = new Point(_tbServer.Left, _tbServer.Top + _tbServer.Height + 30);

            _lblUser = new Label
            {
                Text = "Username:",
                Height = _tbUsername.Height
            };
            _lblUser.Location = new Point(_tbUsername.Left, _tbUsername.Top - _tbUsername.Height);

            _tbPwd = new TextBox
            {
                Width = _tbServer.Width,
                PasswordChar = '*'
            };
            _tbPwd.Location = new Point(_tbUsername.Left, _tbUsername.Top + _tbUsername.Height + 30);

            _lblPwd = new Label
            {
                Text = "Password:",
                Height = _tbPwd.Height
            };
            _lblPwd.Location = new Point(_tbPwd.Left, _tbPwd.Top - _tbPwd.Height);

            _tbDB = new TextBox
            {
                Width = _tbServer.Width
            };
            _tbDB.Location = new Point(_tbServer.Left, _tbPwd.Top + _tbPwd.Height + 30);


            _lblDB = new Label
            {
                Text = "Database (optional):",
                Height = _tbDB.Height,
                Width = _tbDB.Width
            };
            _lblDB.Location = new Point(_tbDB.Left, _tbDB.Top - _tbDB.Height);

            _btnEstablishConnection = new Button
            {
                Text = "Se connecter",
                Size = btnSize
            };
            _btnEstablishConnection.Location = new Point((this.ClientSize.Width - btnSize.Width) / 2, _tbDB.Top + _tbDB.Height + 20);
            _btnEstablishConnection.Click += new EventHandler(BtnEstablishConnection_Click);

            _btnBack = new Button
            {
                Text = "Home",
                Size = new Size(50, 30),
                Location = new Point(10, 10)
            };
            _btnBack.Click += new EventHandler(BtnBack_Click);

            _pnlConnectPage.Controls.AddRange(new Control[] { _tbServer, _lblServer, _tbUsername, _lblUser, _tbPwd, _lblPwd, _tbDB, _lblDB, _btnEstablishConnection, _btnBack});
            #endregion

            #region DBPage
            _pnlDBPage = new Panel
            {
                Size = this.Size
            };

            _tbDB = new TextBox
            {
                Width = this.ClientSize.Width - 100
            };
            _tbDB.Location = new Point((this.ClientSize.Width - _tbDB.Width) / 2, 30);

            _lblDB = new Label
            {
                Text = "Database",
                Height = _tbDB.Height
            };
            _lblDB.Location = new Point(_tbDB.Left, _tbDB.Top - _tbDB.Height);

            _btnCreateDB = new Button
            {
                Text = "Create",
                Size = btnSize,
                Location = new Point((this.ClientSize.Width - btnSize.Width)/2 -btnSize.Width/2 -15, _tbDB.Top + _tbDB.Height + 30)
            };
            _btnCreateDB.Click += new EventHandler(BtnCreateDB_Click);

            _btnDeleteDB = new Button
            {
                Text = "Delete",
                Size = btnSize,
                Location = new Point((this.ClientSize.Width - btnSize.Width) / 2 + btnSize.Width / 2 + 15, _tbDB.Top + _tbDB.Height + 30)
            };
            _btnDeleteDB.Click += new EventHandler(BtnDeleteDB_Click);

            _btnBackDb = new Button
            {
                Text = "Home",
                Size = new Size(50, 30)
            };
            _btnBackDb.Location = new Point(5, (this.ClientSize.Height - _btnBackDb.Height) - 5);
            _btnBackDb.Click += new EventHandler(BtnBack_Click);

            _btnCreateTable = new Button
            {
                Text = "Créer une table",
                Location = new Point(5, 320),
                AutoSize = true
            };
            _btnCreateTable.Click += new EventHandler(BtnCreateTable_Click);
            _pnlDBPage.Controls.AddRange(new Control[] { _tbDB, _lblDB, _btnCreateDB, _btnDeleteDB, _btnBackDb, _btnCreateTable});
            #endregion
        }

        public void BtnConnect_Click(object sender, EventArgs e)
        {
            if(_connection == null || _connection.State == ConnectionState.Closed)
            {
                this.Controls.Remove(_pnlMainPage);

                this.Controls.Add(_pnlConnectPage);

            }
            else
            {
                _connection.Close();
                (sender as Button).Text = "Se connecter";
                MessageBox.Show("Disconected");
            }
        }

        public void BtnEstablishConnection_Click(object sender, EventArgs e)
        {
            try
            {
                _connection = new MySqlConnection("server=" + _tbServer.Text + "; database=" + _tbDB.Text + "; username=" + _tbUsername.Text + "; pwd=" + _tbPwd.Text);

                _connection.Open();

                if (_connection.State == System.Data.ConnectionState.Open)
                {
                    _btnConnect.AutoSize = true;
                    _btnConnect.Text = "Se déconnecter";
                    MessageBox.Show("Connected");
                }
                _btnDb.Enabled = true;
                this.Controls.Remove(_pnlConnectPage);
                this.Controls.Add(_pnlMainPage);
            }
            catch(MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server.  Contact administrator");
                        break;
                    case 1130:
                        MessageBox.Show("The server cannot resolve the hostname of the client.");
                        break;
                    case 1042:
                        MessageBox.Show("Invalid username/password, please try again");
                        break;
                }
            }
        }

        public void BtnBack_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.Controls.Add(_pnlMainPage);
        }

        public void BtnDB_Click(object sender, EventArgs e)
        {
            this.Size = new Size(500, 500);
            this.Controls.Remove(_pnlMainPage);
            this.Controls.Add(_pnlDBPage);
        }

        public void BtnCreateDB_Click(object sender, EventArgs e)
        {
            DB createDB = new DB(_connection, _tbDB.Text, 0);
            Console.WriteLine(_tbDB.Text);
            MessageBox.Show("La base de données " + _tbDB.Text + " a été crée");
        }

        public void BtnDeleteDB_Click(object sender, EventArgs e)
        {
            DB deleteDB = new DB(_connection, _tbDB.Text, 1);
            
            MessageBox.Show("La base de données " + _tbDB.Text + " a été supprimée");
        }
        public void BtnCreateTable_Click(object sender, EventArgs e)
        {
            DB create = new DB(_connection, "Mysql", 1, "yoo", new Dictionary<string, string> { { "hello", "string" }, {"he", "int" } });
        }
    }
}
