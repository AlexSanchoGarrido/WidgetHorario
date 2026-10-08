namespace WidgetHorario.Models;

public static class Horario
{
    public static List<Clase> Todas { get; } = new()
    {
        // ═══════════════════════════════════════
        // LUNES
        // ═══════════════════════════════════════

        new("IPE1", DayOfWeek.Monday,
            "15:20", "16:15",
            "Ma. Fernanda"),

        new("PROG", DayOfWeek.Monday,
            "16:15", "17:10",
            "Dani / Anabel"),

        new("PROG", DayOfWeek.Monday,
            "17:10", "18:05",
            "Dani / Anabel"),

        new("RECREO", DayOfWeek.Monday,
            "18:05", "18:25"),

        new("LMSGI", DayOfWeek.Monday,
            "18:25", "19:20",
            "Antonio"),

        new("SI", DayOfWeek.Monday,
            "19:20", "20:15",
            "Raúl"),

        new("SI", DayOfWeek.Monday,
            "20:15", "21:10",
            "Raúl"),


        // ═══════════════════════════════════════
        // MARTES
        // ═══════════════════════════════════════

        new("IPE1", DayOfWeek.Tuesday,
            "15:20", "16:15",
            "Ma. Fernanda"),

        new("INGLÉS", DayOfWeek.Tuesday,
            "16:15", "17:10",
            "Ana Merino"),

        new("LMSGI", DayOfWeek.Tuesday,
            "17:10", "18:05",
            "Antonio"),

        new("RECREO", DayOfWeek.Tuesday,
            "18:05", "18:25"),

        new("ED", DayOfWeek.Tuesday,
            "18:25", "19:20",
            "Moriano"),

        new("BD", DayOfWeek.Tuesday,
            "19:20", "20:15",
            "Puerto"),

        new("BD", DayOfWeek.Tuesday,
            "20:15", "21:10",
            "Puerto"),


        // ═══════════════════════════════════════
        // MIÉRCOLES
        // ═══════════════════════════════════════

        new("SI", DayOfWeek.Wednesday,
            "15:20", "16:15",
            "Raúl"),

        new("LMSGI", DayOfWeek.Wednesday,
            "16:15", "17:10",
            "Antonio"),

        new("IPE1", DayOfWeek.Wednesday,
            "17:10", "18:05",
            "Ma. Fernanda"),

        new("RECREO", DayOfWeek.Wednesday,
            "18:05", "18:25"),

        new("ED", DayOfWeek.Wednesday,
            "18:25", "19:20",
            "Moriano"),

        new("PROG", DayOfWeek.Wednesday,
            "19:20", "20:15",
            "Dani / Anabel"),

        new("PROG", DayOfWeek.Wednesday,
            "20:15", "21:10",
            "Dani / Anabel"),


        // ═══════════════════════════════════════
        // JUEVES
        // ═══════════════════════════════════════

        new("LMSGI", DayOfWeek.Thursday,
            "15:20", "16:15",
            "Antonio"),

        new("ED", DayOfWeek.Thursday,
            "16:15", "17:10",
            "Moriano"),

        new("BD", DayOfWeek.Thursday,
            "17:10", "18:05",
            "Puerto"),

        new("RECREO", DayOfWeek.Thursday,
            "18:05", "18:25"),

        new("BD", DayOfWeek.Thursday,
            "18:25", "19:20",
            "Puerto"),

        new("SI", DayOfWeek.Thursday,
            "19:20", "20:15",
            "Raúl"),

        new("SI", DayOfWeek.Thursday,
            "20:15", "21:10",
            "Raúl"),


        // ═══════════════════════════════════════
        // VIERNES
        // ═══════════════════════════════════════

        new("PROG", DayOfWeek.Friday,
            "15:20", "16:15",
            "Dani / Anabel"),

        new("PROG", DayOfWeek.Friday,
            "16:15", "17:10",
            "Dani / Anabel"),

        new("PROG", DayOfWeek.Friday,
            "17:10", "18:05",
            "Dani / Anabel"),

        new("RECREO", DayOfWeek.Friday,
            "18:05", "18:25"),

        new("INGLÉS", DayOfWeek.Friday,
            "18:25", "19:20",
            "Ana Merino"),

        new("BD", DayOfWeek.Friday,
            "19:20", "20:15",
            "Puerto"),

        new("BD", DayOfWeek.Friday,
            "20:15", "21:10",
            "Puerto")
    };
}