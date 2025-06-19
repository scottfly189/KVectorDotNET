import { http } from 'uview-plus'

// 生成网页授权Url
export const genAuthUrlApi = (data) => http.post('/api/sysWechat/genAuthUrl', data)

// 获取微信用户OpenId
export const snsOAuth2Api = (data) => http.post('/api/sysWechat/snsOAuth2', data)

// 微信用户登录OpenId
export const openIdLoginApi = (data) => http.post('/api/sysWechat/openIdLogin', data)

// 获取配置签名参数(wx.config)
export const genConfigParaApi = (data) => http.post('/api/sysWechat/genConfigPara', data)

// 获取模板列表
export const messageTemplateListApi = (params) => http.get('/api/sysWechat/messageTemplateList', params)

// 发送模板消息
export const sendTemplateMessageApi = (data) => http.post('/api/sysWechat/sendTemplateMessage', data)

// 删除模板
export const deleteMessageTemplateApi = (data) => http.post('/api/sysWechat/deleteMessageTemplate', data)

