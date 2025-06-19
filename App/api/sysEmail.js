import { http } from 'uview-plus'

// 发送邮件
export const sendEmailApi = (data) => http.post('/api/sysEmail/sendEmail', data)

