import { http } from 'uview-plus'

// 获取通知公告分页列表
export const pageApi = (data) => http.post('/api/sysNotice/page', data)

// 增加通知公告
export const addApi = (data) => http.post('/api/sysNotice/add', data)

// 更新通知公告
export const updateApi = (data) => http.post('/api/sysNotice/update', data)

// 删除通知公告
export const deleteApi = (data) => http.post('/api/sysNotice/delete', data)

// 发布通知公告
export const publicApi = (data) => http.post('/api/sysNotice/public', data)

// 设置通知公告已读状态
export const setReadApi = (data) => http.post('/api/sysNotice/setRead', data)

// 获取接收的通知公告
export const pageReceivedApi = (data) => http.post('/api/sysNotice/pageReceived', data)

// 获取未读的通知公告
export const unReadListApi = (params) => http.get('/api/sysNotice/unReadList', params)

