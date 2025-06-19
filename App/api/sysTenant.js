import { http } from 'uview-plus'

// 获取租户分页列表
export const pageApi = (data) => http.post('/api/sysTenant/page', data)

// 增加租户
export const addApi = (data) => http.post('/api/sysTenant/add', data)

// 设置租户状态
export const setStatusApi = (data) => http.post('/api/sysTenant/setStatus', data)

// 删除租户
export const deleteApi = (data) => http.post('/api/sysTenant/delete', data)

// 更新租户
export const updateApi = (data) => http.post('/api/sysTenant/update', data)

// 授权租户管理员角色菜单
export const grantMenuApi = (data) => http.post('/api/sysTenant/grantMenu', data)

// 获取租户管理员角色拥有菜单Id集合
export const ownMenuListApi = (params) => http.get('/api/sysTenant/ownMenuList', params)

// 重置租户管理员密码
export const resetPwdApi = (data) => http.post('/api/sysTenant/resetPwd', data)

// 同步所有租户数据库
export const syncTenantDbApi = (data) => http.post('/api/sysTenant/syncTenantDb', data)

// 创建租户数据库
export const initTenantDbApi = (data) => http.post('/api/sysTenant/initTenantDb', data)

// 创建租户数据
export const initTenantDataApi = (data) => http.post('/api/sysTenant/initTenantData', data)

// 获取租户下的用户列表
export const userListApi = (data) => http.post('/api/sysTenant/userList', data)

// 获取系统信息
export const sysInfoApi = (params) => http.get('/api/sysTenant/sysInfo', params)

// 保存系统信息
export const saveSysInfoApi = (data) => http.post('/api/sysTenant/saveSysInfo', data)

