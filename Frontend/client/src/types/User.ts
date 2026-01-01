export interface User {
    id: string;
    name: string;
    email: string;
    token: string;
    imageUrl?: string;
}

export interface LoginCreds {
    email: string;
    password: string;
}

export interface RegisterCreds {
    name: string;
    email: string;
    password: string;
}