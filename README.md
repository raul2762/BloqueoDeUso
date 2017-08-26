#Bloqueo De Uso
>Para utilizar este bloqueo de uso se debe copiar las clases **UseLock.cs** y **InputBox.cs** al proyecto que desea utilizar este bloqueo.

>Las imagenes contenidas en este proyecto tambien deben de ser incluidas en el proyecto nuevo y colocar la opcion de compilacion en: **Recurso incrustado**.

>En la clase **InputBox.cs** buscar la funcion o metodo **Picture** dentro de esa funcion o metodo se encuentra un **Swicht** que asigna la imagen del Input Box segun uno la seleccione, en ese swicht en el string que indica la ruta de cada imagen algo parecido a esto **(@"UseLockForms.nic80x80.png")** donde dice **UseLockForms** se debe de cambiar por el nombre de proyecto actual.

>Luego en el constructor **InitializeComponent();** debajo de el colocar **UseLock.ManageAttempt();** para iniciar el sistema de bloqueo de aplicacion. En el evento Load del Forms colocar esto:

```[C#]
if (UseLock.Access_ == false)
            {
                Application.Exit();
            }
            
