using System.Windows.Forms;

namespace DataEditor.Control.Wrapper.Container
{
    /* 盖亚
     * 我们在天上的母亲
     * 愿人尊你的名为圣
     * 愿你的国降临
     * 愿你的旨意行在每一个窗口
     * 如同行在每一个容器
     * 我们所背负的一切
     * 今日赐给我们
     * 包容我们的身躯
     * 如同我们包容世间万物
     * 不叫我们遇见拒绝
     * 救我们脱离变幻
     * 因为，国度、权柄、荣耀
     * 全是你的，直到永远。
     */
    public class Gaia : Control.WrapControlContainer<Panel>
    {
        private Gaia() {}
        private static readonly Gaia instance = new Gaia();
        public static Gaia Instance { get { return instance; } }

        public override string Flag
        {
            get { return "gaia"; }
        }

        public override bool CanAdd(System.Windows.Forms.Control control)
        {
            return false;
        }

        public override void Bind()
        {
            base.Bind();
            Control.Visible = false;
        }


    }
}