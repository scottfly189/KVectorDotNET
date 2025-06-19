import { http } from 'uview-plus'

// 第三方登录
export const signInApi = (params) => http.get('/api/sysOAuth/signIn', params)

// 授权回调
export const signInCallbackApi = (params) => http.get('/api/sysOAuth/signInCallback', params)

