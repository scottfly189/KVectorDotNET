// Admin.NET ��Ŀ�İ�Ȩ���̱ꡢר�����������Ȩ��������Ӧ���ɷ���ı�����ʹ�ñ���ĿӦ������ط��ɷ�������֤��Ҫ��
//
// ����Ŀ��Ҫ��ѭ MIT ���֤�� Apache ���֤���汾 2.0�����зַ���ʹ�á����֤λ��Դ��������Ŀ¼�е� LICENSE-MIT �� LICENSE-APACHE �ļ���
//
// �������ñ���Ŀ����Σ�����Ұ�ȫ��������������ַ����˺Ϸ�Ȩ��ȷ��ɷ����ֹ�Ļ���κλ��ڱ���Ŀ���ο�����������һ�з��ɾ��׺����Σ����ǲ��е��κ����Σ�

namespace Admin.NET.Core.Ai.Service;

public class DataActionInput
{
    public ChatActionEnum ActionType { get; set; }
    public LLMChatSummaryHistory Item { get; set; }
}