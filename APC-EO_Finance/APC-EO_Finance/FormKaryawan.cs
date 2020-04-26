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
using System.IO;
namespace APC_EO_Finance
{
    public partial class FormKaryawan : Form
    {
        public FormKaryawan()
        {
            InitializeComponent();
        }
        public int status = 1;
        private void FormKaryawan_Load(object sender, EventArgs e)
        {
            ClassConnection.Instance().IsConnect();
            ClassConnection.Instance().Close();

            comboBoxJabatan.Items.Add("Kepala Cabang");
            comboBoxJabatan.Items.Add("Ar_Ap");
            comboBoxJabatan.Items.Add("Project Officer Teknis");
            comboBoxJabatan.Items.Add("Project Officer Non Teknis");
            comboBoxJabatan.Items.Add("Accounting");
            comboBoxJabatan.Items.Add("Account Executive");
            comboBoxJabatan.Items.Add("Design");
            comboBoxJabatan.Items.Add("Finance");
            comboBoxJabatan.Items.Add("Petty");
            comboBoxJabatan.Items.Add("Tenaga Produksi");
            comboBoxJabatan.Items.Add("CEO");

            comboBoxArea.Items.Add("Surabaya");
            comboBoxArea.Items.Add("Jakarta");
            comboBoxArea.SelectedIndex = 0;
            textBoxPass.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password; ;

            pictureBoxFotoKaryawan.SizeMode = PictureBoxSizeMode.StretchImage;
            if (status == 1)
            {
                resetKodeKaryawan();
            }
            else if(status == 3)
            {
                textBoxNama.ReadOnly = true;
                textBoxAlamat.ReadOnly = true;
                textBoxNoTelp.ReadOnly = true;
                textBoxEmail.ReadOnly = true;
                textBoxFax.ReadOnly = true;
                textBoxNPWP.ReadOnly = true;
                textBoxPathPhoto.ReadOnly = true;
                textBoxNIK.ReadOnly = true;
                numericUpDownGaji.Enabled = false;
                dateTimePickerLahir.Enabled = false;
                dateTimePickerMasuk.Enabled = false;
                textBoxPathPhoto.ReadOnly = true;
                buttonPhoto.Enabled = false;
                buttonSubmitKaryawan.Visible = false;
                buttonClearKlien.Visible = false;
                textBoxPass.ReadOnly = true;
                textBoxUname.ReadOnly = true;
                textBoxTempat.ReadOnly = true;
                comboBoxArea.Enabled = false;
                comboBoxJabatan.Enabled = false;
            }
        }
        public void resetKodeKaryawan()
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    string kode = "KR" + DateTime.Now.ToString("ddMMyy");
                    MySqlCommand cmd = new MySqlCommand("SELECT MAX(kode) from datakaryawan where kode like '" + kode + "%'", ClassConnection.Instance().Connection);
                    int dataAutoInc = 1;
                    if (cmd.ExecuteScalar().ToString() != "")
                    {
                        dataAutoInc = Convert.ToInt32(cmd.ExecuteScalar().ToString().Substring(8, 4));
                        dataAutoInc += 1;
                    }
                    kode = kode + dataAutoInc.ToString().PadLeft(4, '0');
                    ClassConnection.Instance().Close();
                    textBoxKode.Text = kode;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void tampilData(string kode = "")
        {
            try
            {
                if (ClassConnection.Instance().Connecting())
                {
                    DataTable table = new DataTable();
                    MySqlDataAdapter reader = null;
                    MySqlCommand cmd = new MySqlCommand("SELECT kode, nama, alamat, notelp, email, fax, npwp, foto, nik, gajipokok, date_format(tanggallahir,'%m/%d/%Y'), date_format(tanggalmasuk,'%m/%d/%Y'), jabatan,pass,area,tempatlahir,user FROM datakaryawan where kode ='" + kode + "'", ClassConnection.Instance().Connection);
                    reader = new MySqlDataAdapter(cmd);
                    //reader = cmd.ExecuteReader();
                    reader.Fill(table);
                    textBoxTempat.Text = table.Rows[0][15].ToString();
                    textBoxUname.Text = table.Rows[0][16].ToString();
                    textBoxKode.Text = table.Rows[0][0].ToString();
                    textBoxNama.Text = table.Rows[0][1].ToString();
                    textBoxPass.Text = table.Rows[0][13].ToString();
                    textBoxAlamat.Text = table.Rows[0][2].ToString();
                    textBoxNoTelp.Text = table.Rows[0][3].ToString();
                    textBoxEmail.Text = table.Rows[0][4].ToString();
                    textBoxFax.Text = table.Rows[0][5].ToString();
                    textBoxNPWP.Text = table.Rows[0][6].ToString();
                    if (table.Rows[0][7] != System.DBNull.Value)
                    {
                        byte[] img = (byte[])table.Rows[0][7];
                        pictureBoxFotoKaryawan.Image = byteArrayToImage((Byte[])img);
                    }
                    textBoxNIK.Text = table.Rows[0][8].ToString();
                    numericUpDownGaji.Value = Convert.ToInt32(table.Rows[0][9].ToString());
                    comboBoxJabatan.SelectedIndex = comboBoxJabatan.FindStringExact(table.Rows[0][12].ToString());
                    comboBoxArea.SelectedIndex = comboBoxArea.FindStringExact(table.Rows[0][14].ToString());

                    dateTimePickerLahir.Value = Convert.ToDateTime(table.Rows[0][10].ToString());
                    dateTimePickerMasuk.Value = Convert.ToDateTime(table.Rows[0][11].ToString());

                    ClassConnection.Instance().Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public static Bitmap ByteToImage(byte[] blob)
        {
            MemoryStream mStream = new MemoryStream();
            byte[] pData = blob;
            mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;

        }
        private void ButtonClearKlien_Click(object sender, EventArgs e)
        {
            textBoxNama.Text = "";
            textBoxAlamat.Text = "";
            textBoxNoTelp.Text = "";
            textBoxEmail.Text = "";
            textBoxFax.Text = "";
            textBoxNPWP.Text = "";
            textBoxPathPhoto.Text = "";
            textBoxNIK.Text = "";
            numericUpDownGaji.Value = 0;
            textBoxPathPhoto.Text = "";
            textBoxPass.Text = "";
            textBoxUname.Text = "";
            comboBoxArea.SelectedIndex = 0;
            comboBoxJabatan.SelectedIndex = -1;
            textBoxTempat.Text = "";
            pictureBoxFotoKaryawan = null;
        }

        private void ButtonCloseKlien_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormKaryawan_FormClosing(object sender, FormClosingEventArgs e)
        {
            ((FormParent)this.MdiParent).updateDGVKaryawan();
        }
        private void ButtonSubmitKlien_Click(object sender, EventArgs e)
        {
            if (status == 1)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "INSERT INTO datakaryawan VALUES(@kode,@user,@nama,@pass,@alamat,@notelp,@email,@fax,@npwp,@userimage,@nik,@gajipokok,@tempatlahir,@tanggallahir,@tanggalmasuk,@jabatan,@area,null,null)";
                            if (textBoxPathPhoto.Text == "")
                            {
                                commandText = "INSERT INTO datakaryawan VALUES(@kode,@user,@nama,@pass,@alamat,@notelp,@email,@fax,@npwp,null,@nik,@gajipokok,@tempatlahir,@tanggallahir,@tanggalmasuk,@jabatan,@area,null,null)";
                            }
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKode.Text);
                            cmd.Parameters.AddWithValue("@user", textBoxUname.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNama.Text);
                            cmd.Parameters.AddWithValue("@pass", textBoxPass.Text);
                            cmd.Parameters.AddWithValue("@alamat", textBoxAlamat.Text);
                            cmd.Parameters.AddWithValue("@notelp", textBoxNoTelp.Text);
                            cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                            cmd.Parameters.AddWithValue("@fax", textBoxFax.Text);
                            cmd.Parameters.AddWithValue("@npwp", textBoxNPWP.Text);
                            if (textBoxPathPhoto.Text != "")
                            {
                                var paramUserImage = new MySqlParameter("@userImage", MySqlDbType.Blob, imgToByteArray((Image)pictureBoxFotoKaryawan.Image).Length);
                                paramUserImage.Value = imgToByteArray((Image)pictureBoxFotoKaryawan.Image);
                                cmd.Parameters.Add(paramUserImage);
                            }
                            cmd.Parameters.AddWithValue("@nik", textBoxNIK.Text);
                            cmd.Parameters.AddWithValue("@gajipokok", numericUpDownGaji.Value);
                            cmd.Parameters.AddWithValue("@tempatlahir", textBoxTempat.Text);
                            cmd.Parameters.AddWithValue("@tanggallahir", dateTimePickerLahir.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@tanggalmasuk", dateTimePickerMasuk.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@jabatan", comboBoxJabatan.Text);
                            cmd.Parameters.AddWithValue("@area", comboBoxArea.Text);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            resetKodeKaryawan();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                    
                }
            }
            else if (status == 2)
            {
                if (MessageBox.Show("Apakah anda sudah yakin dengan data tersebut?", "Confirm", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        if (ClassConnection.Instance().Connecting())
                        {
                            string commandText = "UPDATE datakaryawan SET nama = @nama,user = @user,pass = @pass,alamat = @alamat,notelp = @notelp,email = @email,fax = @fax,npwp = @npwp, nik=@nik, gajipokok=@gajipokok,tempatlahir = @tempatlahir, tanggallahir = @tanggallahir, tanggalmasuk = @tanggalmasuk, jabatan = @jabatan, area=@area WHERE kode = @kode";
                            if (textBoxPathPhoto.Text != "")
                            {
                                commandText = "UPDATE datakaryawan SET nama = @nama,user = @user,pass = @pass,alamat = @alamat,notelp = @notelp,email = @email,fax = @fax,npwp = @npwp, foto=@userimage, nik=@nik, gajipokok=@gajipokok,tempatlahir = @tempatlahir, tanggallahir = @tanggallahir, tanggalmasuk = @tanggalmasuk, jabatan = @jabatan, area=@area WHERE kode = @kode";
                            }
                            MySqlCommand cmd = new MySqlCommand(commandText, ClassConnection.Instance().Connection);
                            cmd.Parameters.AddWithValue("@kode", textBoxKode.Text);
                            cmd.Parameters.AddWithValue("@user", textBoxUname.Text);
                            cmd.Parameters.AddWithValue("@nama", textBoxNama.Text);
                            cmd.Parameters.AddWithValue("@pass", textBoxPass.Text);
                            cmd.Parameters.AddWithValue("@alamat", textBoxAlamat.Text);
                            cmd.Parameters.AddWithValue("@notelp", textBoxNoTelp.Text);
                            cmd.Parameters.AddWithValue("@email", textBoxEmail.Text);
                            cmd.Parameters.AddWithValue("@fax", textBoxFax.Text);
                            cmd.Parameters.AddWithValue("@npwp", textBoxNPWP.Text);
                            if (textBoxPathPhoto.Text != "")
                            {
                                var paramUserImage = new MySqlParameter("@userImage", MySqlDbType.Blob, imgToByteArray((Image)pictureBoxFotoKaryawan.Image).Length);
                                paramUserImage.Value = imgToByteArray((Image)pictureBoxFotoKaryawan.Image);
                                cmd.Parameters.Add(paramUserImage);
                            }
                            cmd.Parameters.AddWithValue("@nik", textBoxNIK.Text);
                            cmd.Parameters.AddWithValue("@gajipokok", numericUpDownGaji.Value);
                            cmd.Parameters.AddWithValue("@tempatlahir", textBoxTempat.Text);
                            cmd.Parameters.AddWithValue("@tanggallahir", dateTimePickerLahir.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@tanggalmasuk", dateTimePickerMasuk.Value.ToString("yyyy-MM-dd"));
                            cmd.Parameters.AddWithValue("@jabatan", comboBoxJabatan.Text);
                            cmd.Parameters.AddWithValue("@area", comboBoxArea.Text);
                            int rowsAffected = cmd.ExecuteNonQuery();
                            MessageBox.Show("Data telah Tersimpan", "Berhasil");
                            ClassConnection.Instance().Close();
                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error");
                    }
                }
            }
        }

        //convert image to bytearray
        public byte[] imgToByteArray(Image img)
        {
            using (MemoryStream mStream = new MemoryStream())
            {
                img.Save(mStream, img.RawFormat);
                return mStream.ToArray();
            }
        }
        //convert bytearray to image
        public Image byteArrayToImage(byte[] byteArrayIn)
        {
            using (MemoryStream mStream = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(mStream);
            }
        }

        private void ButtonPhoto_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "All Images Files (*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif)|*.png;*.jpeg;*.gif;*.jpg;*.bmp;*.tiff;*.tif" +
            "|PNG Portable Network Graphics (*.png)|*.png" +
            "|JPEG File Interchange Format (*.jpg *.jpeg *jfif)|*.jpg;*.jpeg;*.jfif" +
            "|BMP Windows Bitmap (*.bmp)|*.bmp" +
            "|TIF Tagged Imaged File Format (*.tif *.tiff)|*.tif;*.tiff" +
            "|GIF Graphics Interchange Format (*.gif)|*.gif";
            openFileDialog1.Title = "Please select an image";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                textBoxPathPhoto.Text = openFileDialog1.FileName;

                //string res = Convert.ToBase64String(imgToByteArray(Image.FromFile(textBoxPathPhoto.Text)));
                //MessageBox.Show(res.Length.ToString());
                //pictureBoxFotoKaryawan.Image = byteArrayToImage(Convert.FromBase64String(res));

                pictureBoxFotoKaryawan.Image = Image.FromFile(textBoxPathPhoto.Text);
            }
        }

        private void TextBoxPathPhoto_Validating(object sender, CancelEventArgs e)
        {
            if (File.Exists(textBoxPathPhoto.Text) || Directory.Exists(textBoxPathPhoto.Text))
            {
                pictureBoxFotoKaryawan.Image = Image.FromFile(textBoxPathPhoto.Text);
            }
            
        }

        private void PictureBoxFotoKaryawan_Click(object sender, EventArgs e)
        {

        }
        bool lihat = false;
        private void ButtonLihat_Click(object sender, EventArgs e)
        {
            if (lihat)
            {
                buttonLihat.Text = "Lihat";
                textBoxPass.UseSystemPasswordChar = PasswordPropertyTextAttribute.Yes.Password; ;
                lihat = false;
            }
            else
            {
                buttonLihat.Text = "Tutup";
                textBoxPass.UseSystemPasswordChar = PasswordPropertyTextAttribute.No.Password; ;
                lihat = true;
            }
        }
    }
}
