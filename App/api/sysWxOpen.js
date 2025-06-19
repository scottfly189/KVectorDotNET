import { http } from 'uview-plus'

// 获取微信用户OpenId
export const wxOpenIdApi = (params) => http.get('/api/sysWxOpen/wxOpenId', params)

// 获取微信用户电话号码
export const wxPhoneApi = (params) => http.get('/api/sysWxOpen/wxPhone', params)

// 微信小程序登录OpenId
export const wxOpenIdLoginApi = (data) => http.post('/api/sysWxOpen/wxOpenIdLogin', data)

// 上传小程序头像
export const uploadAvatarApi = (data) => http.post('/api/sysWxOpen/uploadAvatar', data)

// 
export const setNickNameApi = (data) => http.post('/api/sysWxOpen/setNickName', data)

// 
export const userInfoApi = (params) => http.get('/api/sysWxOpen/userInfo', params)

// 验证签名
export const verifySignatureApi = (params) => http.get('/api/sysWxOpen/verifySignature', params)

// 获取订阅消息模板列表
export const messageTemplateListApi = (params) => http.get('/api/sysWxOpen/messageTemplateList', params)

// 发送订阅消息
export const sendSubscribeMessageApi = (data) => http.post('/api/sysWxOpen/sendSubscribeMessage', data)

// 增加订阅消息模板
export const addSubscribeMessageTemplateApi = (data) => http.post('/api/sysWxOpen/addSubscribeMessageTemplate', data)

// 生成小程序二维码
export const generateQRImageApi = (data) => http.post('/api/sysWxOpen/generateQRImage', data)

