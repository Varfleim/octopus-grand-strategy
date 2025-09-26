
using UnityEngine;

namespace GS.UI
{
    public class UI_GameWindow : MonoBehaviour
    {
        public GameObject activeMainPanel;

        public UI_ObjectPanel objectPanel;

        public void HideMainPanel()
        {
            //�������� �������� ������� ������
            activeMainPanel.SetActive(false);

            //���������, ��� �������� ������� ������ ���
            activeMainPanel = null;
        }

        public void MainPanelActivation(
            GameObject requestedPanel,
            out bool isSamePanel)
        {
            //�������� �� ��������� ������������
            isSamePanel = false;

            //���� ������� ����������� ������
            if(activeMainPanel == requestedPanel)
            {
                //��������, ��� ���� ������� �� �� ������
                isSamePanel = true;
            }

            //���� ���� ������� �� �� ������
            if(isSamePanel == true)
            {
                
            }
            //�����
            else
            {
                //���������� ����������� ������

                //���� �����-���� ������ ���� �������, �������� �
                if(activeMainPanel != null)
                {
                    activeMainPanel.SetActive(false);
                }

                //������ ����������� ������ ��������
                requestedPanel.SetActive(true);

                //��������� � ��� �������� ������
                activeMainPanel = requestedPanel;
            }
        }
    }
}
