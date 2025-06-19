import { http } from 'uview-plus'

// 账号密码登录
export const loginApi = (data) => http.post('/api/appAuth/login', data)

// 手机号登录
export const loginPhoneApi = (data) => http.post('/api/appAuth/loginPhone', data)

// 获取登录账号
export const userInfoApi = (params) => http.get('/api/appAuth/userInfo', params)

// 获取刷新Token
export const refreshTokenApi = (params) => http.get('/api/appAuth/refreshToken', params)

// 退出系统
export const logoutApi = (data) => http.post('/api/appAuth/logout', data)

// 获取验证码
export const captchaApi = (params) => http.get('/api/appAuth/captcha', params)

// 修改用户密码
export const changePwdApi = (data) => http.post('/api/appAuth/changePwd', data)

