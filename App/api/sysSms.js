import { http } from 'uview-plus'

// 发送短信
export const sendSmsApi = (data) => http.post('/api/sysSms/sendSms', data)

// 校验短信验证码
export const verifyCodeApi = (data) => http.post('/api/sysSms/verifyCode', data)

// 阿里云发送短信
export const aliyunSendSmsApi = (data) => http.post('/api/sysSms/aliyunSendSms', data)

// 发送短信模板
export const aliyunSendSmsTemplateApi = (data) => http.post('/api/sysSms/aliyunSendSmsTemplate', data)

// 腾讯云发送短信
export const tencentSendSmsApi = (data) => http.post('/api/sysSms/tencentSendSms', data)

