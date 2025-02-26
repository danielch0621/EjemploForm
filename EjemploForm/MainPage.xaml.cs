using RestSharp;

namespace EjemploForm
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void btnGuardar_Clicked(object sender, EventArgs e)
        {
            Persona per = new Persona();
            per.Nombre = txtNombre.Text;
            per.Edad = int.Parse(txtEdad.Text);
            per.Ocupacion = txtOcupacion.Text;
            per.Direccion = txtDireccion.Text;

            RestClient cliente = new RestClient();
            RestRequest peticion = new RestRequest("https://localhost:44306/api/Personas/", Method.Post);

            peticion.AddBody(per);

            var respuesta = cliente.ExecuteAsync(peticion);

            DisplayAlert("", "Guardado con exito", "Ok");
        }

        private void btnLista_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ListaRegistros());
        }
    }
}
