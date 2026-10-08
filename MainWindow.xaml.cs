using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using WidgetHorario.Models;
using System.Windows.Media.Effects;

namespace WidgetHorario;

public partial class MainWindow : Window
{
    private readonly DispatcherTimer _timer;
    private bool _siempreVisible = false;


    private void CambiarSiempreVisible(bool activar)
    {
        _siempreVisible = activar;
        Topmost = activar;
    }
    private readonly string[] _dias =
    {
        "LUN",
        "MAR",
        "MIÉ",
        "JUE",
        "VIE"
    };

    private readonly DayOfWeek[] _diasSemana =
    {
        DayOfWeek.Monday,
        DayOfWeek.Tuesday,
        DayOfWeek.Wednesday,
        DayOfWeek.Thursday,
        DayOfWeek.Friday
    };

    private readonly string[] _horas =
    {
    "15:20",
    "16:15",
    "17:10",
    "18:05",
    "18:25",
    "19:20",
    "20:15"
};

    public MainWindow()
    {
        InitializeComponent();

        CrearHorario();

        _timer = new DispatcherTimer
        {
            Interval = TimeSpan.FromSeconds(1)
        };

        _timer.Tick += Actualizar;
        _timer.Start();

        Actualizar(null, EventArgs.Empty);
    }

    private void CrearHorario()
    {
        HorarioGrid.Children.Clear();
        HorarioGrid.RowDefinitions.Clear();
        HorarioGrid.ColumnDefinitions.Clear();

        // Columna de horas
        HorarioGrid.ColumnDefinitions.Add(
            new ColumnDefinition
            {
                Width = new GridLength(80)
            });

        // Cinco días
        for (int i = 0; i < 5; i++)
        {
            HorarioGrid.ColumnDefinitions.Add(
                new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
        }

        // Cabecera + filas
        HorarioGrid.RowDefinitions.Add(
            new RowDefinition
            {
                Height = new GridLength(45)
            });

        HorarioGrid.RowDefinitions.Add(
    new RowDefinition
    {
        Height = new GridLength(55, GridUnitType.Star)
    });

        HorarioGrid.RowDefinitions.Add(
            new RowDefinition
            {
                Height = new GridLength(55, GridUnitType.Star)
            });

        HorarioGrid.RowDefinitions.Add(
            new RowDefinition
            {
                Height = new GridLength(55, GridUnitType.Star)
            });

        // RECREO
        HorarioGrid.RowDefinitions.Add(
            new RowDefinition
            {
                Height = new GridLength(20, GridUnitType.Star)
            });

        HorarioGrid.RowDefinitions.Add(
            new RowDefinition
            {
                Height = new GridLength(55, GridUnitType.Star)
            });

        HorarioGrid.RowDefinitions.Add(
            new RowDefinition
            {
                Height = new GridLength(55, GridUnitType.Star)
            });

        HorarioGrid.RowDefinitions.Add(
            new RowDefinition
            {
                Height = new GridLength(55, GridUnitType.Star)
            });

        // Esquina superior izquierda
        AñadirTexto(
            "",
            0,
            0,
            "#18181B",
            "#66666F");

        // Cabeceras de días
        for (int dia = 0; dia < 5; dia++)
        {
            AñadirTexto(
                _dias[dia],
                0,
                dia + 1,
                "#27272A",
                "White");
        }

        // Horas y celdas
        for (int fila = 0; fila < _horas.Length; fila++)
        {
            AñadirTexto(
                _horas[fila],
                fila + 1,
                0,
                "#18181B",
                "#777780");

            for (int dia = 0; dia < 5; dia++)
            {
                CrearCelda(
                    _horas[fila],
                    _diasSemana[dia],
                    fila + 1,
                    dia + 1);
            }
        }
    }

    private void CrearCelda(
    string horaTexto,
    DayOfWeek dia,
    int fila,
    int columna)
    {
        var hora = TimeSpan.Parse(horaTexto);

        var clase = Horario.Todas.FirstOrDefault(c =>
            c.Dia == dia &&
            c.Inicio == hora);

        var contenedor = new Grid
        {
            Margin = new Thickness(2)
        };

        // Fondo base de la celda
        var fondo = new Border
        {
            Background = new SolidColorBrush(
                Color.FromRgb(30, 30, 34)),

            BorderBrush = new SolidColorBrush(
                Color.FromRgb(55, 55, 60)),

            BorderThickness = new Thickness(1),
            CornerRadius = new CornerRadius(8)
        };

        contenedor.Children.Add(fondo);

        if (clase != null)
        {
            // ─────────────────────────────
            // RECREO
            // ─────────────────────────────

            if (clase.EsRecreo)
            {
                fondo.Background = new SolidColorBrush(
                    Color.FromRgb(45, 45, 48));

                fondo.BorderBrush = new SolidColorBrush(
                    Color.FromRgb(70, 70, 75));

                var textoRecreo = new TextBlock
                {
                    Text = "☕  RECREO",
                    Foreground = new SolidColorBrush(
                        Color.FromRgb(150, 150, 155)),

                    FontSize = 12,
                    FontWeight = FontWeights.SemiBold,

                    HorizontalAlignment =
                        HorizontalAlignment.Center,

                    VerticalAlignment =
                        VerticalAlignment.Center
                };

                contenedor.Children.Add(textoRecreo);
            }

            // ─────────────────────────────
            // ASIGNATURA
            // ─────────────────────────────

            else
            {
                var progreso = new Border
                {
                    Background =
                        ObtenerColorAsignatura(clase),

                    VerticalAlignment =
                        VerticalAlignment.Top,

                    HorizontalAlignment =
                        HorizontalAlignment.Stretch,

                    Height = 0,

                    CornerRadius =
                        new CornerRadius(8),

                    Tag = clase
                };

                contenedor.Children.Add(progreso);

                // Borde exterior
                var borde = new Border
                {
                    Background = Brushes.Transparent,

                    BorderBrush =
                        new SolidColorBrush(
                            Color.FromRgb(55, 55, 60)),

                    BorderThickness = new Thickness(1),

                    CornerRadius =
                        new CornerRadius(8)
                };

                contenedor.Children.Add(borde);

                // Texto
                var panelTexto = new StackPanel
                {
                    HorizontalAlignment =
                        HorizontalAlignment.Center,

                    VerticalAlignment =
                        VerticalAlignment.Center
                };

                var nombre = new TextBlock
                {
                    Text = clase.Asignatura,

                    Foreground = Brushes.White,

                    FontSize = 15,

                    FontWeight =
                        FontWeights.SemiBold,

                    HorizontalAlignment =
                        HorizontalAlignment.Center
                };

                panelTexto.Children.Add(nombre);

                var profesor = new TextBlock
                {
                    Text = clase.Profesor,

                    Foreground =
                        new SolidColorBrush(
                            Color.FromRgb(210, 210, 215)),

                    FontSize = 11,

                    HorizontalAlignment =
                        HorizontalAlignment.Center,

                    Margin =
                        new Thickness(0, 3, 0, 0)
                };

                panelTexto.Children.Add(profesor);

                contenedor.Children.Add(panelTexto);
            }
        }

        Grid.SetRow(contenedor, fila);
        Grid.SetColumn(contenedor, columna);

        HorarioGrid.Children.Add(contenedor);
    }

    private SolidColorBrush ObtenerColorAsignatura(Clase clase)
    {
        return clase.Asignatura switch
        {
            "PROG" => new SolidColorBrush(Color.FromRgb(124, 92, 255)),
            "BD" => new SolidColorBrush(Color.FromRgb(55, 140, 240)),
            "SI" => new SolidColorBrush(Color.FromRgb(45, 180, 120)),
            "LMSGI" => new SolidColorBrush(Color.FromRgb(240, 150, 55)),
            "IPE1" => new SolidColorBrush(Color.FromRgb(220, 80, 110)),
            "ED" => new SolidColorBrush(Color.FromRgb(180, 80, 200)),
            "INGLÉS" => new SolidColorBrush(Color.FromRgb(50, 180, 200)),

            _ => new SolidColorBrush(Color.FromRgb(100, 100, 105))
        };
    }

    private void AñadirTexto(
        string texto,
        int fila,
        int columna,
        string fondo,
        string color)
    {
        var border = new Border
        {
            Background = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(fondo)),

            Margin = new Thickness(2),
            CornerRadius = new CornerRadius(8)
        };

        var textBlock = new TextBlock
        {
            Text = texto,
            Foreground = new SolidColorBrush(
                (Color)ColorConverter.ConvertFromString(color)),

            FontSize = 14,
            FontWeight = FontWeights.SemiBold,

            HorizontalAlignment = HorizontalAlignment.Center,
            VerticalAlignment = VerticalAlignment.Center
        };

        border.Child = textBlock;

        Grid.SetRow(border, fila);
        Grid.SetColumn(border, columna);

        HorarioGrid.Children.Add(border);
    }

    private void Actualizar(object? sender, EventArgs e)
    {
        DateTime ahora = DateTime.Now;

        HoraText.Text = ahora.ToString("HH:mm:ss");

        FechaText.Text = ahora.ToString(
            "dddd, d 'de' MMMM",
            new System.Globalization.CultureInfo("es-ES"));

        ActualizarProgreso(ahora);
    }

    private void ActualizarProgreso(DateTime ahora)
    {
        foreach (var elemento in HorarioGrid.Children)
        {
            if (elemento is not Grid grid)
                continue;

            Clase? claseEncontrada = null;
            Border? progreso = null;
            Border? borde = null;

            foreach (var hijo in grid.Children)
            {
                if (hijo is Border b)
                {
                    if (b.Tag is Clase clase)
                    {
                        claseEncontrada = clase;
                        progreso = b;
                    }
                    else if (b.BorderBrush != null)
                    {
                        borde = b;
                    }
                }
            }

            if (claseEncontrada == null ||
                progreso == null)
                continue;

            var claseActual = claseEncontrada;

            // Esperamos a que WPF conozca el tamaño real
            if (grid.ActualHeight <= 0)
                continue;

            // ─────────────────────────────
            // CLASE TERMINADA
            // ─────────────────────────────

            if (claseActual.EstaTerminada(ahora))
            {
                progreso.Height = grid.ActualHeight;

                progreso.Background =
                    ObtenerColorAsignatura(claseActual);

                if (borde != null)
                {
                    borde.BorderBrush =
                        ObtenerColorAsignatura(claseActual);
                }

                continue;
            }

            // ─────────────────────────────
            // CLASE ACTUAL
            // ─────────────────────────────

            if (claseActual.EstaActiva(ahora))
            {
                double porcentaje =
                    claseActual.Progreso(ahora);

                progreso.Height =
                    grid.ActualHeight * porcentaje;

                progreso.Background =
                    ObtenerColorAsignatura(claseActual);

                // BRILLO
                if (borde != null)
                {
                    var color =
                        ObtenerColorAsignatura(claseActual).Color;

                    borde.BorderBrush =
                        new SolidColorBrush(color);

                    borde.BorderThickness =
                        new Thickness(2);
                    borde.Effect = new DropShadowEffect
                    {
                        Color =
        ObtenerColorAsignatura(claseActual).Color,

                        BlurRadius = 15,

                        ShadowDepth = 0,

                        Opacity = 0.85
                    };
                }

                continue;
            }

            // ─────────────────────────────
            // CLASE FUTURA
            // ─────────────────────────────

            progreso.Height = 0;

            if (borde != null)
            {
                borde.BorderBrush =
                    new SolidColorBrush(
                        Color.FromRgb(55, 55, 60));

                borde.BorderThickness =
                    new Thickness(1);
            }
        }
    }

    private void Window_MouseLeftButtonDown(
        object sender,
        MouseButtonEventArgs e)
    {
        DragMove();
    }
}