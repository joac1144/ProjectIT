using ProjectIT.Shared.Enums;

namespace ProjectIT.Shared.Models;

public class Semester : IComparable<Semester>
{
    public Season Season { get; set; }

    public int Year { get; set; }

    public override string ToString()
    {
        return $"{Season} {Year}";
    }

    public static bool operator ==(Semester semester1, Semester semester2)
    {
        if (semester1 is null || semester2 is null)
            return false;
        return semester1.Season == semester2.Season && semester1.Year == semester2.Year;
    }

    public static bool operator !=(Semester semester1, Semester semester2)
    {
        return !(semester1 == semester2);
    }

    public static bool operator >(Semester semester1, Semester semester2)
    {
        return semester1.Year > semester2.Year || (semester1.Year == semester2.Year && semester1.Season > semester2.Season);
    }

    public static bool operator <(Semester semester1, Semester semester2)
    {
        return semester1.Year < semester2.Year || (semester1.Year == semester2.Year && semester1.Season < semester2.Season);
    }

    public int CompareTo(Semester? other)
    {
        if (other is null)
        {
            return 1;
        }

        if (this == other)
        {
            return 0;
        }
        else if (this > other)
        {
            return 1;
        }
        else
        {
            return -1;
        }
    }

    public override bool Equals(object? obj)
    {
        if (this == (Semester)obj!)
            return true;
        else
            return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Season, Year);
    }
}