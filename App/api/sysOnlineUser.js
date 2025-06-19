import { http } from 'uview-plus'

// 获取在线用户分页列表
export const pageApi = (data) => http.post('/api/sysOnlineUser/page', data)

// 强制下线
export const forceOfflineApi = (data) => http.post('/api/sysOnlineUser/forceOffline', data)

