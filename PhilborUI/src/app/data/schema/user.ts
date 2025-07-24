export interface AppUser {
    id: number;
    userName: string;
    firstName: string;
    lastName: string;
    gender: string;
    email: string;
    twoFactorEnabled: boolean;
    roles: string[];
  }
  
  export interface LoginResponse {
    isSuccess: boolean;
    isFailure: boolean;
    message: string | null;
    metaMessage?: string | null;
    error?: {
      code: string;
      description: string;
      status: number | null;
    };
    validationErrors?: any;
    data: {
      token: string | null;
      expiration: string | null;
      user: AppUser;
    };
    metaData?: any;
  } 