import { http } from 'uview-plus'

// 发送消息给所有人
export const sendAllUserApi = (data) => http.post('/api/sysMessage/sendAllUser', data)

// 发送消息给除了发送人的其他人
export const sendOtherUserApi = (data) => http.post('/api/sysMessage/sendOtherUser', data)

// 发送消息给某个人
export const sendUserApi = (data) => http.post('/api/sysMessage/sendUser', data)

// 发送消息给某些人
export const sendUsersApi = (data) => http.post('/api/sysMessage/sendUsers', data)

