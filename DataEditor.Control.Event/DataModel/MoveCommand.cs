using DataEditor.FuzzyData;
using System;
using System.Collections.Generic;

namespace DataEditor.Control.Event.DataModel
{
    public class MoveCommand : Command
    {
        static new public FuzzyData.FuzzySymbol ClassName = FuzzyData.FuzzySymbol.GetSymbol("RPG::MoveCommand");
        public const string Sign = "□";

        public MoveCommand(CommandType Origin)
            : base(Origin)
        {
            Link.ClassName = ClassName;
            Link.InstanceVariables.Remove(IndentSymbol);
        }

        public MoveCommand(FuzzyObject command)
            : base(command)
        {
            object obj;
            var fuzzy_code = command.InstanceVariables.TryGetValue(CodeSymbol, out obj) ? obj as FuzzyData.FuzzyFixnum : null;
            if (fuzzy_code != null) this.Type = CommandType.TryGetCommandType(Convert.ToInt32(fuzzy_code.Value), "Move");
        }

        public override void SyncToLink()
        {
            if (!isChecked)
            {
                if (!(Link.InstanceVariables.ContainsKey(ParametersSymbol)))
                    Link.InstanceVariables.Add(ParametersSymbol, new FuzzyArray());
                if (!(Link.InstanceVariables[ParametersSymbol] is FuzzyArray))
                    Link.InstanceVariables[ParametersSymbol] = new FuzzyArray();
                isChecked = true;
            }
            var FuzzyParameter = Link.InstanceVariables[ParametersSymbol] as FuzzyArray;
            FuzzyParameter.Clear();
            Parameters.ForEach((x) => FuzzyParameter.Add(x));
        }

        public override string GenerateString()
        {
            if (Type == null) return "Unknown Command";
            if (Type.Text == null) return Sign + Type.Name;
            if (Code == 0) return "";
            List<FuzzyObject>[] target = new List<FuzzyObject>[2] { Parameters, new List<FuzzyObject>() };
            return Sign + Type.Name + " : " + Type.Text.ToString(target);
        }
    }
}