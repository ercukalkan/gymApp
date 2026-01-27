export interface JWTPayload {
    userId: string,
    email: string,
    isAdmin: string | boolean
}