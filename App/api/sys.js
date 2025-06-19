import { http } from 'uview-plus'

// GoView 登录
export const loginApi = (data) => http.post('/api/goview/sys/login', data)

// GoView 退出
export const logoutApi = (params) => http.get('/api/goview/sys/logout', params)

// 获取 OSS 上传接口
export const getOssInfoApi = (params) => http.get('/api/goview/sys/getOssInfo', params)

