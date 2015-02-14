using System;
using System.Collections.Generic;
using System.Text;
using DataEditor.FuzzyData;

namespace DataEditor.Control.Event.DataModel
{
    static public class Audio
    {
        static FuzzySymbol NameSymbol = FuzzySymbol.GetSymbol("@name"),
            VolumeSymbol = FuzzySymbol.GetSymbol("@volume"),
            PitchSymbol = FuzzySymbol.GetSymbol("@pitch"),
            ClassNameSymbol = FuzzySymbol.GetSymbol("RPG::AudioFile");
        static public FuzzyObject GetAudio(string Name = "", int Volume = 0, int Pitch = 0)
        {
            FuzzyObject fobj = new FuzzyObject();
            fobj.ClassName = ClassNameSymbol;
            fobj.InstanceVariables.Add(NameSymbol, new FuzzyString(Name));
            fobj.InstanceVariables.Add(VolumeSymbol, new FuzzyFixnum(Volume));
            fobj.InstanceVariables.Add(PitchSymbol, new FuzzyFixnum(Pitch));
            return fobj;
        }
        static public FuzzyObject GetAudio(string Description)
        {
            if (Description == null || Description == "") return GetAudio();
            string[] parts = Description.Split(',');
            string name = parts[0];
            int volume = 0, pitch = 0;
            if (parts.Length == 1 || !(int.TryParse(parts[1], out volume))) return GetAudio(name);
            if (parts.Length == 2 || !(int.TryParse(parts[2], out pitch))) return GetAudio(name, volume);
            return GetAudio(name, volume, pitch);
        }
        static public bool SetParameter<T>(FuzzyObject audio, FuzzySymbol symbol, T obj)
        {
            bool ans = audio.InstanceVariables.ContainsKey(symbol);
            if (ans)
                audio[symbol] = obj;
            else audio.InstanceVariables.Add(symbol, obj);
            return ans;
        }
        static public bool SetName(FuzzyObject audio, string name) { return SetParameter<FuzzyString>(audio, NameSymbol, new FuzzyString(name)); }
        static public bool SetVolume(FuzzyObject audio, int volume) { return SetParameter<FuzzyFixnum>(audio, VolumeSymbol, new FuzzyFixnum(volume)); }
        static public bool SetPitch(FuzzyObject audio, int pitch) { return SetParameter<FuzzyFixnum>(audio, PitchSymbol, new FuzzyFixnum(pitch)); }
    }
}
