using BuscaCEP.Models;
using System.Text.Json;

namespace BuscaCEP
{
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void btnPesquisa_Click(object sender, EventArgs e)
        {
            buscaCEP();
        }

        public async void buscaCEP()
        {
            try
            {
                CEP cep = new CEP();
                string txtCEP = mtxtCEP.Text;
                if (!string.IsNullOrEmpty(txtCEP))
                {
                    txtCEP = txtCEP.Replace("-", "");
                    string URICEP = $"https://viacep.com.br/ws/{txtCEP}/json/";
                    using (var client = new HttpClient())
                    {
                        using (var response = await client.GetAsync((URICEP)))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                var content = await response.Content.ReadAsStringAsync();
                                var options = new JsonSerializerOptions
                                {
                                    IncludeFields = true,
                                    PropertyNameCaseInsensitive = true
                                };
                                cep = JsonSerializer.Deserialize<CEP>(content, options);
                            }
                            else
                            {
                                throw new Exception("Não foi possível obter o CEP: " + response.StatusCode);
                            }
                        }
                    }
                }

                if (cep != null && cep.erro != "true")
                {

                    txtBairro.Text = cep.Bairro.ToString();
                    txtLogradouro.Text = cep.Logradouro.ToString();
                    txtBairro.Text = cep.Bairro.ToString();
                    txtMunicipio.Text = cep.Localidade.ToString();
                    txtUF.Text = cep.UF.ToString();
                    textCEP.Text = cep.Cep.ToString();
                }
                else
                {
                    MessageBox.Show("CEP Inexistente!", "Atenção!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro!");
            }
        }

        private void mtxtCEP_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                buscaCEP();
            }
        }

        private void mtxtCEP_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab && mtxtCEP.Focused)
            {
                buscaCEP();
                
            }
        }
    }
}