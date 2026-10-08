namespace WidgetHorario.Models;

public class Clase
{
    public string Asignatura { get; set; } = "";
    public DayOfWeek Dia { get; set; }

    public TimeSpan Inicio { get; set; }
    public TimeSpan Fin { get; set; }

    public string Profesor { get; set; } = "";
    public string Aula { get; set; } = "";

    public bool EsRecreo =>
        Asignatura.Equals("RECREO", StringComparison.OrdinalIgnoreCase);

    public Clase(
        string asignatura,
        DayOfWeek dia,
        string inicio,
        string fin,
        string profesor = "",
        string aula = "219")
    {
        Asignatura = asignatura;
        Dia = dia;
        Inicio = TimeSpan.Parse(inicio);
        Fin = TimeSpan.Parse(fin);
        Profesor = profesor;
        Aula = aula;
    }

    public bool EstaTerminada(DateTime ahora)
{
    // Si el día ya ha pasado
    if (Dia < ahora.DayOfWeek)
        return true;

    // Si es un día futuro
    if (Dia > ahora.DayOfWeek)
        return false;

    // Estamos en el mismo día
    return ahora.TimeOfDay >= Fin;
}

    public bool EstaActiva(DateTime ahora)
    {
        return ahora.DayOfWeek == Dia &&
               ahora.TimeOfDay >= Inicio &&
               ahora.TimeOfDay < Fin;
    }

    public double Progreso(DateTime ahora)
    {
        if (!EstaActiva(ahora))
            return 0;

        double duracion = (Fin - Inicio).TotalSeconds;
        double transcurrido =
            (ahora.TimeOfDay - Inicio).TotalSeconds;

        return Math.Clamp(transcurrido / duracion, 0, 1);
    }

    public TimeSpan TiempoRestante(DateTime ahora)
    {
        if (!EstaActiva(ahora))
            return TimeSpan.Zero;

        return Fin - ahora.TimeOfDay;
    }
}