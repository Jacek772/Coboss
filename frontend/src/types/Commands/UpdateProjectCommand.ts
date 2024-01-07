type UpdateProjectCommand = {
  id: number
  name?: string
  description?: string
  term?: Date
  managerId?: number
}

export default UpdateProjectCommand