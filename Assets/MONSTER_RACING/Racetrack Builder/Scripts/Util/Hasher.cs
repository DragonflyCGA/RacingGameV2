using UnityEngine;
/// <summary>
/// Helper class for calculating basic hashes.
/// </summary>
public class Hasher 
{
    public int Hash { get; private set; } = 17;

    public Hasher Int(int value)
    {
        unchecked // Don't error on overflow
        {
            Hash = Hash * 23 + value;
        }

        return this;
    }

    public Hasher Object(object o)
    {
        return Int(o != null ? o.GetHashCode() : 0);
    }

    public Hasher Float(float f)
    {
        // Note: If just using Int(f.GetHashCode()) then negated sequences can produce the same hash as their non negated version.
        // E.g. Float(-9.0f).Float(-0.2f) == Float(9.0f).Float(0.2f) !

        // Thus hash negatives differently
        if (f < 0.0f)
        {
            Int(7607);              // (Prime #)
            f = -f;                 // Hash positive value
        }

        return Int(f.GetHashCode());
    }

    public Hasher Bool(bool b)
    {
        return Int(b.GetHashCode());
    }

    public Hasher RoundedFloat(float f)
    {
        return Float(RacetrackUtil.RoundedFloat(f));
    }

    public Hasher Vector3(UnityEngine.Vector3 v)
    {
        return RoundedFloat(v.x).RoundedFloat(v.y).RoundedFloat(v.z);
    }

    public Hasher RoundedMatrix4x4(Matrix4x4 m)
    {
        for (int i = 0; i < 16; i++)
            RoundedFloat(m[i]);
        return this;
    }
}
