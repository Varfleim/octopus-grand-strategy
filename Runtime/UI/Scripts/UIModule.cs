
using UnityEngine;

using GBB;

namespace GS.UI
{
    [CreateAssetMenu]
    public class UIModule : GameModule
    {
        public override void AddSystems(GameStartup startup)
        {
            //��������� ���������� �������
            #region Frame
            //���� � ������ �������
            startup.AddFrameSystem(new SObjectPanelInput());
            #endregion

            //��������� ������� ����������
            #region PreRender
            //����������� ������ �������
            startup.AddPreRenderSystem(new SObjectPanelControl());

            //����������� ������� ����� ��������
            startup.AddPreRenderSystem(new SObjectMapPanelControl());
            #endregion

            //��������� ��������� �������
            #region PostTick
            //���������� ������� � ���� ���� � ����� ������� ����
            startup.AddPostTickSystem(new S_GameWindowTickUpdate());
            #endregion
        }

        public override void InjectData(GameStartup startup)
        {
            //���� ��������� ������ UI
            UIData uIData = startup.GetComponentInChildren<UIData>();

            //������ ������
            startup.InjectData(uIData);

            //���� ������� ������ ����������
            UICore uICore = uIData.uICore;

            //������ ������
            startup.InjectData(uICore);
        }
    }
}
