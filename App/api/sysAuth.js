import { http } from 'uview-plus'

// 账号密码登录
export const loginApi = (data) => http.post('/api/sysAuth/login', data)

// 验证锁屏密码
export const unLockScreenApi = (data) => http.post('/api/sysAuth/unLockScreen', data)

// 手机号登录
export const loginPhoneApi = (data) => http.post('/api/sysAuth/loginPhone', data)

// 获取登录账号
export const userInfoApi = (params) => http.get('/api/sysAuth/userInfo', params)

// 获取刷新Token
export const refreshTokenApi = (params) => http.get('/api/sysAuth/refreshToken', params)

// 退出系统
export const logoutApi = (data) => http.post('/api/sysAuth/logout', data)

// 获取验证码
export const captchaApi = (params) => http.get('/api/sysAuth/captcha', params)

// Swagger登录检查
export const swaggerCheckUrlApi = (data) => http.post('/api/swagger/checkUrl', data)

// Swagger登录提交
export const swaggerSubmitUrlApi = (data) => http.post('/api/swagger/submitUrl', data)

