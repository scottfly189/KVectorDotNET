import { http } from 'uview-plus'

// 获取用户分页列表
export const pageApi = (data) => http.post('/api/sysUser/page', data)

// 增加用户
export const addApi = (data) => http.post('/api/sysUser/add', data)

// 更新用户
export const updateApi = (data) => http.post('/api/sysUser/update', data)

// 删除用户
export const deleteApi = (data) => http.post('/api/sysUser/delete', data)

// 查看用户基本信息
export const baseInfoApi = (params) => http.get('/api/sysUser/baseInfo', params)

// 更新用户基本信息
export const updateBaseInfoApi = (data) => http.post('/api/sysUser/updateBaseInfo', data)

// 设置用户状态
export const setStatusApi = (data) => http.post('/api/sysUser/setStatus', data)

// 授权用户角色
export const grantRoleApi = (data) => http.post('/api/sysUser/grantRole', data)

// 修改用户密码
export const changePwdApi = (data) => http.post('/api/sysUser/changePwd', data)

// 重置用户密码
export const resetPwdApi = (data) => http.post('/api/sysUser/resetPwd', data)

// 验证密码有效期
export const verifyPwdExpirationTimeApi = (data) => http.post('/api/sysUser/verifyPwdExpirationTime', data)

// 解除登录锁定
export const unlockLoginApi = (data) => http.post('/api/sysUser/unlockLogin', data)

// 获取用户拥有角色集合
export const ownRoleListApi = (params) => http.get('/api/sysUser/ownRoleList', params)

// 获取用户扩展机构集合
export const ownExtOrgListApi = (params) => http.get('/api/sysUser/ownExtOrgList', params)

