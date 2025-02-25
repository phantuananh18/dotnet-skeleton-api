import User from '../entities/User';
import BaseRepository from './BaseRepository';

export default class UserRepository extends BaseRepository<User> {
    private static _instance: UserRepository;

    constructor() {
        super(User);
    }

    /**
     * Get existing repository instance. If it does not exist, create one
     * @returns The repository instance
     */
    static getInstance() {
        if(!UserRepository._instance) {
            UserRepository._instance = new UserRepository();
        }

        return UserRepository._instance;
    }
    
    async findUserByUserId(userId) {
        return await this.dataSouceRepository.findOneBy({
            userId
        });
    }

    async createUser(user) {
        return await this.dataSouceRepository.save(user);
    }

    async findUserByCriteria(criteriaList, condition = 'and') {
        let query = this.dataSouceRepository.createQueryBuilder('user');
        switch (condition) {
            case 'and':
                for (const criteria of criteriaList) {
                    query = query.andWhere(`user.${criteria.field} = :value`, { value: criteria.value });
                }
                break;
            case 'or':
                query = query.where(builder => {
                    criteriaList.forEach((criteria, index) => {
                        if (index === 0) {
                            builder.orWhere(`user.${criteria.field} = :value`, { value: criteria.value });
                        } else {
                            builder.orWhere(`user.${criteria.field} = :value${index}`, { [`value${index}`]: criteria.value });
                        }
                    });
                });
                break;
        }

        return await query.getOne();
    }

    async findUserAndRelatedDataByCriteria(criteriaName, criteriaValue) {
        const whereCondition = {};
        whereCondition[criteriaName] = criteriaValue; 
        return await this.dataSouceRepository
            .createQueryBuilder('user')
            .innerJoinAndSelect('user.role', 'role')
            .where(whereCondition)
            .getOne();
    }

    async findUserWithSearch (keyword) {
        return await this.dataSouceRepository
            .createQueryBuilder('user')
            .orWhere('user.firstName like :keyword', { keyword:`%${keyword}%` } )
            .orWhere('user.lastName like :keyword', { keyword:`%${keyword}%` } )
            .orWhere('user.email like :keyword', { keyword:`%${keyword}%` })
            .innerJoinAndSelect('user.role', 'role')
            .getMany();
    }
}