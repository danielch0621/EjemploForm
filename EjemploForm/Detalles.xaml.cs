using RestSharp;

namespace EjemploForm;

public partial class Detalles : ContentPage
{
    Persona p;
	public Detalles()
	{
		InitializeComponent();
	}

    public Detalles(Persona _p)
    {
        InitializeComponent();
        p = _p;
        Loaded += Detalles_Loaded;
    }

    private void Detalles_Loaded(object? sender, EventArgs e)
    {
        lblId.Text = p.Id.ToString();
        lblNombre.Text = p.Nombre;
        lblEdad.Text = p.Edad.ToString();
        lblOcupacion.Text = p.Ocupacion;
        lblDireccion.Text = p.Direccion;
    }

    private void btnRegresar_Clicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }

    private async void btnActualizar_Clicked(object sender, EventArgs e)
    {
        RestClient cliente = new RestClient();
        RestRequest peticion = new RestRequest("https://localhost:44306/api/Personas/"+lblId.Text,Method.Put);

        Persona p = new Persona();
        p.Id = int.Parse(lblId.Text);
        p.Nombre = lblNombre.Text;
        p.Edad = int.Parse(lblEdad.Text);
        p.Ocupacion = lblOcupacion.Text;
        p.Direccion = lblDireccion.Text;
        peticion.AddBody(p);

        var res = await cliente.ExecutePutAsync(peticion);

        if(res.ResponseStatus == ResponseStatus.Completed)
        {
            await DisplayAlert("Mensaje", "Actualizado con exito", "Ok");

            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Ocurrio un error al guardar", "Ok");
        }

        
    }
}