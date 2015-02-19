using System.Collections.Generic;
using DataEditor.FuzzyData;

namespace DataEditor.Control.Event.DataModel
{
    public static class MoveRoute
    {
        public static FuzzySymbol ClassName = FuzzySymbol.GetSymbol("RPG::MoveRoute");
        public static FuzzySymbol SkippableSymbol = FuzzySymbol.GetSymbol("@skippable");
        public static FuzzySymbol RepeatSymbol = FuzzySymbol.GetSymbol("@repeat");
        public static FuzzySymbol ListSymbol = FuzzySymbol.GetSymbol("@list");
        public static FuzzyObject CreateRoute()
        {
            FuzzyObject ans = new FuzzyObject();
            ans.ClassName = ClassName;
            ans.InstanceVariables.Add(SkippableSymbol, new FuzzyBool());
            ans.InstanceVariables.Add(RepeatSymbol, new FuzzyBool());
            return ans;
        }

        public static bool getSkippable(FuzzyObject route)
        {
            var fuzzy_skippable = route[SkippableSymbol] as FuzzyBool;
            return fuzzy_skippable != null && fuzzy_skippable.Value;
        }

        public static void setSkippable(FuzzyObject route, bool Value)
        {
            var fuzzy_skippable = route[SkippableSymbol] as FuzzyBool;
            if (fuzzy_skippable != null) fuzzy_skippable.Value = Value;
        }

        public static bool getRepeat(FuzzyObject route)
        {
            var fuzzy_repeat = route[RepeatSymbol] as FuzzyBool;
            return fuzzy_repeat != null && fuzzy_repeat.Value;
        }

        public static void setRepeat(FuzzyObject route, bool Value)
        {
            var fuzzy_repeat = route[RepeatSymbol] as FuzzyBool;
            if (fuzzy_repeat != null) fuzzy_repeat.Value = Value;
        }

        public static void setList(FuzzyObject route, List<MoveCommand> commands)
        {
            if (!(route.InstanceVariables.ContainsKey(ListSymbol)) || !(route[ListSymbol] is FuzzyArray))
                route.InstanceVariables.Add(ListSymbol, new FuzzyArray());
            var target = route.InstanceVariables[ListSymbol] as FuzzyArray;
            target.Clear();
            commands.ForEach(command => target.Add(command.Link));
        }
    }
}