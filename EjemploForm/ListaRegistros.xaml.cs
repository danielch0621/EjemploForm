using RestSharp;
using System.Data;
using Newtonsoft.Json;


namespace EjemploForm;

public partial class ListaRegistros : ContentPage
{
	public ListaRegistros()
	{
		InitializeComponent();
        Loaded += ListaRegistros_Loaded;
	}
    private async void ListaRegistros_Loaded(object? sender, EventArgs e)
    {
        cargar();
    }

    private void txtBuscar_TextChanged(object sender, TextChangedEventArgs e)
    {

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        var fila = (Grid)boton.Parent;
        Label id = (Label)fila[0];
        Label nombre = (Label)fila[1];
        Label edad = (Label)fila[2];
        Label ocupacion = (Label)fila[3];
        Label direccion = (Label)fila[4];

        Persona p = new Persona();
        p.Id = int.Parse(id.Text);
        p.Nombre = nombre.Text;
        p.Edad = int.Parse(edad.Text);
        p.Ocupacion = ocupacion.Text;
        p.Direccion = direccion.Text;

        Navigation.PushAsync(new Detalles(p));

    }

    private async void btnBorrar_Clicked(object sender, EventArgs e)
    {
        Button boton = (Button)sender;
        var fila = (Grid)boton.Parent;
        Label lblid = (Label)fila[0];
        int id = int.Parse(lblid.Text);
        RestClient cliente = new RestClient();
        RestRequest peticion = new RestRequest("https://localhost:44306/api/Personas/"+id,Method.Delete);
        var res = await cliente.ExecuteDeleteAsync(peticion);
        await DisplayAlert("Mensaje", "Registro eliminado", "Ok");
        cargar();
    }

    private async void cargar()
    {
        RestClient cliente = new RestClient();
        RestRequest peticion = new RestRequest("https://localhost:44306/api/personas/", Method.Get);

        var respuesta = await cliente.ExecuteGetAsync(peticion);

        var res = (List<Persona>)JsonConvert.DeserializeObject(respuesta.Content, typeof(List<Persona>));

        DataTable dt = new DataTable();
        dt.Columns.Add("Id");
        dt.Columns.Add("Nombre");
        dt.Columns.Add("Edad");
        dt.Columns.Add("Ocupacion");
        dt.Columns.Add("Direccion");

        foreach (Persona p in res)
        {
            DataRow fila = dt.NewRow();
            fila[0] = p.Id;
            fila[1] = p.Nombre;
            fila[2] = p.Edad;
            fila[3] = p.Ocupacion;
            fila[4] = p.Direccion;
            dt.Rows.Add(fila);
        }
        Lista.ItemsSource = dt.Rows;
    }
}